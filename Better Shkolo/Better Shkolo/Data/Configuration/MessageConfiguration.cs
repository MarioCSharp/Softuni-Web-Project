using Better_Shkolo.Data.Enums;
using Better_Shkolo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Better_Shkolo.Data.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(c => c.SentTo).HasConversion(c => c.ToString(), c => Enum.Parse<SentTo>(c));
        }
    }
}
