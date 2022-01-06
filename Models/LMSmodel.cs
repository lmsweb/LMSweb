using System;
using System.Data.Entity;
using System.Linq;

namespace LMSweb.Models
{
    public class LMSmodel : DbContext
    {
        public LMSmodel(): base("name=LMSmodel")
        {
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<KnowledgePoint> KnowledgePoints { get; set; }
        public DbSet<LearningBehavior> LearnB { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<PeerAssessment> PeerA { get; set; }
        public DbSet<SelfAssessment> SelfA { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentMission> StudentMissions { get; set; }
        public DbSet<TeacherAssessment> TeacherA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

}