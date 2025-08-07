using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efCore.Model.CompanyClasses
{
    #region Data Annotation
    [Table("Employees", Schema = "dbo")]
    #endregion
    public class Employee
    {
        #region Conventions Extra Information
        /* A Set of default rules that automatically configure your model based on common patterns
         * 
         * Table Name Convention:
         *      - Based on the DbSet property name in the DbContext
         *      - If no DbSet exists, the class name is used
         *      
         * Attributes Convention:
         *      - Based on the property type and name
         *      - int -> int
         *      - long -> bigint
         *      - string? -> nvarchar(max) and allows null
         *      - DateTime -> datetime2
         *      - bool -> bit
         *      - decimal -> decimal(18,2)
         *      - byte[] -> varbinary(max)
         *      
         * PK Conventions: 
         *      - A property named [ClassName]Id (case-insensitive) becomes the primary key
         *      - If multiple properties match, Id takes precedence over [ClassName]Id
         *      - If multiple properties match, but not found Id => throw exception
         *      - To make the table without PK => Use Fluent API
         * 
         * FK Conventions:
         *      - [NavigationPropertyName][ParentPKName] : DeptDepartmentId 
         *      - [NavigationPropertyName]Id : DeptId
         *      - [ParentEntityName][ParentPKName] : DepartmentDepartmentId
         *      - [ParentEntityName]Id : DepartmentId
         */
        //Integer and long PK is Identity by default
        //public int EmployeeId { get; set; } // Conventions: PK => [ClassName]Id
        //public string Name { get; set; } //nvarchar(max) by default
        //public int? Age { get; set; } //int and allows null    
        //public string? Address { get; set; } //Nullable string
        #endregion

        #region Data Annotations
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Not Identity
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Auto-increment
        public int EmpSSN { get; set; } // Conventions: PK => [ClassName]Id

        [Required]
        //[MaxLength(50)]
        [StringLength(50, ErrorMessage = "EmpName must be in range(3 - 50)")]
        [Column("EmpName", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(10 , 2)")]
        public decimal Salary { get; set; }

        public int? Age { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Address is Required")]
        public string? Address { get; set; }

        [NotMapped]
        public string Anything { get; set; }

        //Search about Validation Attributes
        #endregion

        #region Foreign Key
        [ForeignKey("Dept")]
        public int DepartmentId { get; set; }
        #endregion

        #region Navigation Properties
        //[ForeignKey("DepartmentId")] //Another Approach
        public virtual Department Dept { get; set; }

        public virtual List<Project>? projects{ get; set; } = new List<Project>();


        #endregion

        public override string ToString()
        {
            return $"{EmpSSN}::{Name}::{Salary}::{Dept.Name}::{DepartmentId}";
        }
    }
}
