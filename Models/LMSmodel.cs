using System;
using System.Data.Entity;
using System.Linq;

namespace LMSweb.Models
{
    public class LMSmodel : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'LMSmodel' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'LMSweb.Models.LMSmodel' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'LMSmodel' 連接字串。
        public LMSmodel(): base("name=LMSmodel")
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<KnowledgePoint> KnowledgePoints { get; set; }
        public DbSet<LearningBehavior> LearnB { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<PeerAssessment> PeerA { get; set; }
        public DbSet<Prompt> Prompts { get; set; }
        public DbSet<SelfAssessment> SelfA { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentMission> StudentMissions { get; set; }
        public DbSet<TeacherAssessment> TeacherA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}