using AutoMapper;
using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Absence;
using Better_Shkolo.Models.Absences;
using Better_Shkolo.Models.Mark;
using Better_Shkolo.Models.Parent;
using Better_Shkolo.Models.Review;
using Better_Shkolo.Models.Student;
using Better_Shkolo.Models.Subject;
using Better_Shkolo.Models.Test;
using Better_Shkolo.Services.AccountService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager;
        private IAccountService accountService;
        private IMapper mapper;
        public StudentService(ApplicationDbContext context,
                              UserManager<User> userManager,
                              IMapper mapper,
                              IAccountService accountService)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
            this.accountService = accountService;
        }
        public async Task<bool> Add(StudentCreateModel model)
        {
            var studentUser = await context.Users.FindAsync(model.UserId);
            var school = await context.Schools.FindAsync(model.SchoolId);
            var grade = await context.Grades.FindAsync(model.GradeId);

            if (grade is null)
            {
                return false;
            }

            var teacher = await context.Teachers.FindAsync(grade.TeacherId);

            if (studentUser is null || school is null || teacher is null)
            {
                return false;
            }

            var student = mapper.Map<Student>(model);
            student.GradeTeacherId = grade.TeacherId;

            studentUser.DoctorAddress = model.DoctorAddress;
            await context.SaveChangesAsync();
            studentUser.DoctorName = model.DoctorName;
            await context.SaveChangesAsync();
            studentUser.DoctorPhone = model.DoctorPhone;
            await context.SaveChangesAsync();

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            await userManager.AddToRoleAsync(await context.Users.FindAsync(student.UserId), "Student");

            return await context.Students.ContainsAsync(student);
        }

        public async Task<bool> AsignParent(ParentCreateModel model, int id)
        {
            var student = await context.Students.FindAsync(id);

            if (student is null)
            {
                return false;
            }

            var parent = new Parent()
            {
                UserId = model.UserId,
                StudentId = id
            };

            await context.Parents.AddAsync(parent);
            await context.SaveChangesAsync();

            var user = await context.Users.FindAsync(parent.UserId);

            await userManager.AddToRoleAsync(user, "Parent");

            var p = await context.Parents.FirstOrDefaultAsync(x => x.UserId == model.UserId);

            student.ParentId = p.Id;
            await context.SaveChangesAsync();

            return await context.Parents.ContainsAsync(parent);
        }

        public async Task<bool> Delete(int id)
        {
            var student = await context.Students.FindAsync(id);

            if (student is null)
            {
                return false;
            }

            var studentUser = await context.Users.FindAsync(student.UserId);

            var parents = await context.Parents.Where(x => x.StudentId == id).ToListAsync();

            foreach (var parent in parents)
            {
                context.Parents.Remove(parent);
                await context.SaveChangesAsync();

                var user = await context.Users.FindAsync(parent.UserId);

                await userManager.RemoveFromRoleAsync(user, "Parent");
            }

            await userManager.RemoveFromRoleAsync(studentUser, "Student");

            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return !await context.Students.ContainsAsync(student);
        }

        public async Task<bool> Edit(StudentCreateModel model, int id)
        {
            var student = await GetStudent(id);

            if (student is null)
            {
                return false;
            }

            student.GradeId = model.GradeId;
            await context.SaveChangesAsync();

            var user = await context.Users.FindAsync(student.UserId);

            user.DoctorPhone = model.DoctorPhone;
            await context.SaveChangesAsync();
            user.DoctorName = model.DoctorName;
            await context.SaveChangesAsync();
            user.DoctorAddress = model.DoctorAddress;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<(string, double)> GetBestSubject(int place)
        {
            var s = await GetStudent(accountService.GetUserId());

            if (s is null)
            {
                s = await GetStudentFromParent(accountService.GetUserId());
            }

            var marks = await context.Marks.Where(x => x.StudentId == s.Id).ToListAsync();

            var standings = new Dictionary<int, double>();

            foreach (var mark in marks)
            {
                if (!standings.ContainsKey(mark.SubjectId))
                {
                    standings[mark.SubjectId] = marks.Where(x => x.SubjectId == mark.SubjectId).Average(x => x.Value);
                }
            }

            var oB = standings.OrderByDescending(x => x.Value).ToArray()[place - 1];

            var ss = await context.Subjects.FindAsync(oB.Key);

            return ($"{ss.Name}", double.Parse($"{oB.Value:F2}"));
        }

        public async Task<Student> GetStudent(int id)
        {
            return await context.Students.FindAsync(id);
        }

        public async Task<Student> GetStudent(string id)
        {
            return await context.Students.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Student> GetStudentFromParent(string userId)
        {
            var p = await context.Parents.FirstOrDefaultAsync(x => x.UserId == userId);

            return await context.Students.FindAsync(p.StudentId);
        }

        public async Task<AbsencesAddModel> GetStudentModel(int id)
        {
            var student = await context.Students.FindAsync(id);
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == student.UserId);

            var model = new AbsencesAddModel();

            model.Id = id;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.SchoolId = student.SchoolId;

            return model;
        }

        public async Task<StudentProfileModel> GetStudentProfile(string userId, int term)
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            if (student is null)
            {
                student = await GetStudentFromParent(userId);
            }

            if (student == null) return null;

            var model = new StudentProfileModel();

            model.Marks = await context.Marks
                .Where(x => x.StudentId == student.Id && x.Term == term)
                .Select(x => new MarkOverallModel()
                {
                    Id = x.Id,
                    Value = x.Value,
                    TeacherFullName = x.Teacher.User.FirstName + " " + x.Teacher.User.LastName,
                    SubjectName = x.Subject.Name,
                    AddedOn = x.AddedOn,
                    SubjectId = x.Subject.Id,
                    Term = x.Term
                })
                .ToListAsync();

            model.Absences = await context.Absencess
                .Where(x => x.StudentId == student.Id)
                .Select(x => new AbsencesOverallModel()
                {
                    Id = x.Id,
                    AddedOn = x.AddedOn,
                    ExcusedOn = x.ExcusedOn,
                    TeacherFullName = x.Teacher.User.FirstName + " " + x.Teacher.User.LastName,
                    SubjectName = x.Subject.Name,
                    SubjectId = x.Subject.Id
                })
                .ToListAsync();

            model.Reviews = await context.Reviews
                .Where(x => x.StudentId == student.Id)
                .Select(x => new ReviewOverallModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    AddedOn = x.AddedOn,
                    TeacherFullName = x.Teacher.User.FirstName + " " + x.Teacher.User.LastName,
                    SubjectName = x.Subject.Name,
                    SubjectId = x.Subject.Id
                })
                .ToListAsync();

            model.Tests = await context.Tests
                .Where(x => x.GradeId == student.GradeId)
                .Select(x => new TestOverallModel()
                {
                    Id = x.Id,
                    AddedOn = x.AddedOn,
                    TestDate = x.TestDate,
                    TeacherFullName = x.Teacher.User.FirstName + " " + x.Teacher.User.LastName,
                    SubjectName = x.Subject.Name,
                    SubjectId = x.Subject.Id
                })
                .ToListAsync();

            model.AllSubjects = await context.Subjects
                .Where(x => x.GradeId == student.GradeId)
                .Select(x => new SubjectDisplayModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    SubjectId = x.Id,
                    TeacherId = x.TeacherId
                })
                .ToListAsync();

            var termMarks = new Dictionary<int, (int, int)>();

            foreach (var subject in model.AllSubjects)
            {
                var tM = await context.TermMarks.FirstOrDefaultAsync(x => x.StudentId == student.Id && subject.Id == x.SubjectId && x.Term == term);

                if (tM != null)
                {
                    termMarks.Add(subject.Id, (tM.Term, tM.Value));
                }
            }

            model.SubjectTermMark = termMarks;

            return model;
        }

        public async Task<StudentViewModel> GetStudentsInGrade(int id)
        {
            var res = new StudentViewModel();
            res.Students = await context.Students.Where(x => x.GradeId == id)
                .Select(x => new StudentDisplayModel()
                {
                    Id = x.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    SchoolId = x.SchoolId,
                    SchoolName = x.School.Name,
                    UserId = x.UserId,
                }).ToListAsync();

            return res;
        }

        public async Task<List<StudentDisplayModel>> GetStudentsInSchool(int id)
        {
            return await context.Students.Where(x => x.SchoolId == id)
                .Select(x => new StudentDisplayModel
                {
                    Id = x.Id,
                    FirstName = context.Users.FirstOrDefault(y => y.Id == x.UserId).FirstName,
                    LastName = context.Users.FirstOrDefault(y => y.Id == x.UserId).LastName,
                    Email = context.Users.FirstOrDefault(y => y.Id == x.UserId).Email,
                    SchoolId = x.SchoolId,
                    SchoolName = context.Schools.FirstOrDefault(y => y.Id == x.SchoolId).Name,
                }).ToListAsync();
        }

        public async Task<List<StudentDisplayModel>> GetStudentsInSubject(int id)
        {
            var subject = await context.Subjects.FindAsync(id);

            return await context.Students.Where(x => x.GradeId == subject.GradeId)
                .Select(x => new StudentDisplayModel()
                {
                    Id = x.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    SchoolId = x.SchoolId,
                    SchoolName = x.School.Name,
                }).ToListAsync();
        }
    }
}
