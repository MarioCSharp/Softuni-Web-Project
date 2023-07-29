using Better_Shkolo.Controllers;
using Better_Shkolo.Models.Absence;
using MyTested.AspNetCore.Mvc;

namespace Better_Shkolo.Test.Controllers
{
    public class AbsencesControllerTests
    {
        [Fact]
        public void AddShouldReturnViewWithModelAndCorrectData()
            => MyController<AbsencesController>
                 .Instance(controller => 
                     controller.WithUser(user => user.InRole("Teacher"))
                     .WithData(new Student() { Id = 33 }, new Subject() { Id = 41 }))
                 .Calling(x => x.Add(33, 41))
                 .ShouldReturn()
                 .View(result =>
                        result.WithModelOfType<AbsencesAddModel>()
                        .Passing(model =>
                        {
                            Assert.Equal(41, model.SubjectId);
                            Assert.Equal(33, model.Id);
                        }));


    }
}