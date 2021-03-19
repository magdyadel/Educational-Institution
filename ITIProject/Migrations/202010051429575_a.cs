namespace ITIProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cources",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Free = c.Boolean(nullable: false),
                        Cost = c.Double(nullable: false),
                        Hours = c.Int(nullable: false),
                        Degree = c.Int(nullable: false),
                        MinDegree = c.Int(nullable: false),
                        Course_Department_ID = c.Int(),
                        Course_Professor_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.Course_Department_ID)
                .ForeignKey("dbo.Professors", t => t.Course_Professor_ID)
                .Index(t => t.Course_Department_ID)
                .Index(t => t.Course_Professor_ID);
            
            CreateTable(
                "dbo.Students_Cources",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Student_ID = c.Int(),
                        Course_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.Student_ID)
                .ForeignKey("dbo.Cources", t => t.Course_ID)
                .Index(t => t.Student_ID)
                .Index(t => t.Course_ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 50),
                        Level = c.Int(nullable: false),
                        BirthYear = c.Int(nullable: false),
                        Student_Department_ID = c.Int(),
                        Student_Professor_supervisior_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Professors", t => t.Student_Professor_supervisior_ID)
                .ForeignKey("dbo.Departments", t => t.Student_Department_ID)
                .Index(t => t.Student_Department_ID)
                .Index(t => t.Student_Professor_supervisior_ID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Manager_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Professors", t => t.Manager_ID, cascadeDelete: true)
                .Index(t => t.Manager_ID);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 50),
                        Salary = c.Double(nullable: false),
                        Professor_Department_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.Professor_Department_ID)
                .Index(t => t.Professor_Department_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Students_Cources", "Course_ID", "dbo.Cources");
            DropForeignKey("dbo.Students_Cources", "Student_ID", "dbo.Students");
            DropForeignKey("dbo.Departments", "Manager_ID", "dbo.Professors");
            DropForeignKey("dbo.Students", "Student_Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Professors", "Professor_Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Students", "Student_Professor_supervisior_ID", "dbo.Professors");
            DropForeignKey("dbo.Cources", "Course_Professor_ID", "dbo.Professors");
            DropForeignKey("dbo.Cources", "Course_Department_ID", "dbo.Departments");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Professors", new[] { "Professor_Department_ID" });
            DropIndex("dbo.Departments", new[] { "Manager_ID" });
            DropIndex("dbo.Students", new[] { "Student_Professor_supervisior_ID" });
            DropIndex("dbo.Students", new[] { "Student_Department_ID" });
            DropIndex("dbo.Students_Cources", new[] { "Course_ID" });
            DropIndex("dbo.Students_Cources", new[] { "Student_ID" });
            DropIndex("dbo.Cources", new[] { "Course_Professor_ID" });
            DropIndex("dbo.Cources", new[] { "Course_Department_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Professors");
            DropTable("dbo.Departments");
            DropTable("dbo.Students");
            DropTable("dbo.Students_Cources");
            DropTable("dbo.Cources");
        }
    }
}
