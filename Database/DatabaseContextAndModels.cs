using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class JournalDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ClassGroup> ClassGroups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Material> Materials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=*****;Username=postgres;Password=*****;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.StudentId);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.User)
                .WithOne(u => u.Teacher)
                .HasForeignKey<Teacher>(t => t.TeacherId);
            
            modelBuilder.Entity<User>()
                .Property(u => u.DateCreated)
                .HasColumnName("date_created");
            

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Subjects)
                .HasForeignKey(s => s.TeacherId);

            modelBuilder.Entity<ClassGroup>()
                .HasOne(cg => cg.Teacher)
                .WithMany(t => t.classgroups)
                .HasForeignKey(cg => cg.TeacherId);

            modelBuilder.Entity<TeacherSubject>()
                .HasKey(ts => new { ts.TeacherId, ts.SubjectId });

            modelBuilder.Entity<TeacherSubject>()
                .HasOne(ts => ts.Teacher)
                .WithMany(t => t.TeacherSubjects)
                .HasForeignKey(ts => ts.TeacherId);

            modelBuilder.Entity<TeacherSubject>()
                .HasOne(ts => ts.Subject)
                .WithMany(s => s.TeacherSubjects)
                .HasForeignKey(ts => ts.SubjectId);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Subject)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.SubjectId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SubjectId);
            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName)
                .HasColumnName("role_name");

            modelBuilder.Entity<Schedule>()
                .HasOne(sch => sch.Subject)
                .WithMany(s => s.Schedules)
                .HasForeignKey(sch => sch.SubjectId);

            modelBuilder.Entity<Schedule>()
                .HasOne(sch => sch.ClassGroup)
                .WithMany(cg => cg.Schedules)
                .HasForeignKey(sch => sch.ClassGroupId);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Schedule)
                .WithMany(sch => sch.Materials)
                .HasForeignKey(m => m.ScheduleId);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Subject)
                .WithMany(s => s.Materials)
                .HasForeignKey(m => m.SubjectId);
        }
    }

     [Table("roles")]
    public class Role
    {
        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }

    [Table("users")]
    public class User
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public Role Role { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
    }

    [Table("subjects")]
    public class Subject
    {
        [Column("subject_id")]
        public int SubjectId { get; set; }

        [Column("subject_name")]
        public string SubjectName { get; set; }

        [Column("teacher_id")]
        public int? TeacherId { get; set; }

        public Teacher Teacher { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Material> Materials { get; set; }
        public List<TeacherSubject> TeacherSubjects { get; set; }
        public List<Attendance> Attendances { get; set; }
        public List<Grade> Grades { get; set; }
    }

    [Table("classgroups")]
    public class ClassGroup
    {
        [Column("class_group_id")]
        public int ClassGroupId { get; set; }

        [Column("class_group_name")]
        public string ClassGroupName { get; set; }

        [Column("teacher_id")]
        public int? TeacherId { get; set; }

        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }
        public List<Schedule> Schedules { get; set; }
    }

    [Table("students")]
    public class Student
    {
        [Column("student_id")]
        public int StudentId { get; set; }

        public User User { get; set; }

        [Column("class_group_id")]
        public int ClassGroupId { get; set; }

        public ClassGroup ClassGroup { get; set; }
        public List<Attendance> Attendances { get; set; }
        public List<Grade> Grades { get; set; }
    }

    [Table("teachers")]
    public class Teacher
    {
        [Column("teacher_id")]
        public int TeacherId { get; set; }

        public User User { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<ClassGroup> classgroups { get; set; }
        public List<TeacherSubject> TeacherSubjects { get; set; }
    }

    [Table("teachersubjects")]
    public class TeacherSubject
    {
        [Column("teacher_id")]
        public int TeacherId { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }

        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }

    [Table("attendance")]
    public class Attendance
    {
        [Column("attendance_id")]
        public int AttendanceId { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }

    [Table("grades")]
    public class Grade
    {
        [Column("grade_id")]
        public int GradeId { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        [Column("grade_value")]
        public int GradeValue { get; set; }

        [Column("grade_date")]
        public DateTime GradeDate { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }

    [Table("schedule")]
    public class Schedule
    {
        [Column("schedule_id")]
        public int ScheduleId { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        [Column("class_group_id")]
        public int ClassGroupId { get; set; }

        public ClassGroup ClassGroup { get; set; }

        [Column("day_of_week")]
        public string DayOfWeek { get; set; }

        [Column("start_time")]
        public TimeSpan StartTime { get; set; }

        [Column("end_time")]
        public TimeSpan EndTime { get; set; }

        public List<Material> Materials { get; set; }
    }

    [Table("materials")]
    public class Material
    {
        [Column("material_id")]
        public int MaterialId { get; set; }

        [Column("schedule_id")]
        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        [Column("homework")]
        public string Homework { get; set; }

        [Column("topic_of_the_lesson")]
        public string TopicOfTheLesson { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("publish_date")]
        public DateTime PublishDate { get; set; }
    }
}
