using efCore.Model.CompanyClasses;
using efCore.Model.SchoolClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efCore
{
    public class SchoolContext : DbContext
    {
        #region entities
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=session2Material;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

        // for lazy loading add {   .UseLazyLoadingProxies();   }  after the conction string
        #endregion

        #region Data-Annotaion
        [Key]
        public int StudentId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdentityId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? OptionalNote { get; set; }
        [StringLength(50)]
        public string Name2 { get; set; }

        [MinLength(3)]
        [MaxLength(10)]
        public string Username { get; set; }

        [Column("Full_Name", TypeName = "nvarchar(100)")]
        public string Name3 { get; set; }

        [Display(Name = "Full Name")]
        public string Name4 { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        [RegularExpression(@"^[A-Z][a-z]*$")]
        public string FirstName { get; set; }

        [NotMapped]
        public string TempValue { get; set; }
        #endregion

        #region fluent API
       /* public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Person>();

            // 1. Exclude Entity From Model

            modelBuilder.Ignore<TemporaryData>();
            // 2. Set Default Value
            modelBuilder.Entity<Person>().Property(p => p.IsActive)
                .HasDefaultValue(true);

            // 3. Set Identity for Keys with data type that is not int, long
            entity.Property(p => p.Uid)
            .ValueGeneratedOnAdd()
            .HasDefaultValue(0);

            // 4. Change Table Name
            entity.ToTable("hh");

            // 5. Change Column Name
            entity.Property(p => p.LastName)
                .HasColumnName("name");

            // 6. Change Column Data Type
            entity.Property(p => p.LastName)
                .HasColumnType("");

            // 7. Set Property Max Length
            entity.Property(p => p.LastName)
            .HasMaxLength(50);

            // 8. Set Column Comment (EF Core 7.0+)
            entity.Property(p => p.LastName)
               .HasComment("jjjj");

            // 9. Change Primary Key
            entity.HasKey(p => p.LastName);

            // 10. Change Primary Key Name
            entity.HasKey(p => p.LastName)
                .HasName("name");


            // 11. Composite Key
            entity.HasKey(p => new { p.Uid, p.LastName });*/

            // 12. Set Default Value (again - different usage)

            //====================================================
            /* // relation 
             // 1- one-to-many 

             modelBuilder.Entity<Grade>()
                 .HasMany<Student>(g => g.Students)
                 .WithOne(s => s.Grade)
                 .HasForeignKey(s => s.GradeId)
                 .OnDelete(DeleteBehavior.Cascade);

             // 2- one-to-one

             modelBuilder.Entity<Student>()
            .HasOne<StudentAddress>(s => s.Address)
            .WithOne(ad => ad.Student)
            .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);

             // 3- many-to-many

              modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.SId, sc.CId });

                 modelBuilder.Entity<StudentCourse>()
                 .HasOne<Student>(sc => sc.Student)
                 .WithMany(s => s.StudentCourses)
                 .HasForeignKey(sc => sc.SId);


                 modelBuilder.Entity<StudentCourse>()
                 .HasOne<Course>(sc => sc.Course)
                 .WithMany(s => s.StudentCourses)
                 .HasForeignKey(sc => sc.CId);

 */

       // }
        #endregion

        #region HandsOn

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                // 1. Change table name
                entity.ToTable("tbl_Students");

                // 2. Ignore property (even if it's not in the class)
                entity.Ignore("MiddleName"); // for demo

                // 3. Add identity key on Guid
                entity.Property<Guid>("StudentCode")
                      .ValueGeneratedOnAdd();

                // 4. Use StudentCode as new PK
                entity.HasKey("StudentCode")
                      .HasName("PK_Student_Code");

                // 5. Rename FirstName column
                entity.Property(e => e.FirstName)
                      .HasColumnName("student_first_name");

                // 6. Max length for LastName
                entity.Property(e => e.LastName)
                      .HasMaxLength(50);

                // 7. Change column type for Height
                entity.Property(e => e.Height)
                      .HasColumnType("decimal(5,2)");

                // 8. Comment on Weight
                entity.Property(e => e.Weight)
                      .HasComment("Weight in kilograms");

                // 9. Default value for IsActive
                entity.Property<bool>("IsActive")
                      .HasDefaultValue(true);

                // 10. Optional composite key (comment if not needed)
                entity.HasKey("StudentCode", "DateOfBirth");

                // Relationship
                modelBuilder.Entity<Student>()
                   .HasOne<Grade>(s => s.Grade)
                   .WithMany(g => g.Students)
                   .HasForeignKey(s => s.GradeId)
                   .OnDelete(DeleteBehavior.Cascade);
            });
            // Grade entity config
            modelBuilder.Entity<Grade>(entity =>
                {
                    entity.HasKey(g => g.Id);

                    entity.Property(g => g.GradeName)
                          .IsRequired()
                          .HasMaxLength(20);

                    entity.Property(g => g.Section)
                          .HasMaxLength(10);
                });









        }
        #endregion
    }
   

}
