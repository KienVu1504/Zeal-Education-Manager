Create Database ZealEducationManager

use ZealEducationManager

create table Class (
	ClassId int primary key identity(1,1) not null,
	ClassName varchar(50) null
)

create table Subject (
	SubjectId int primary key identity(1,1) not null,
	ClassId int foreign key references Class (ClassId) null,
	SubjectName varchar(50) null
)

Create table Student (
	StudentId int primary key identity(1,1) not null,
	Name varchar(50) null,
	DOB date null,
	Gender varchar(50) null,
	Mobile bigint null,
	RollNo varchar(50) null,
	Address varchar(max) null,
	ClassId int foreign key references Class (ClassId) null
)

create table Teacher(
	TeacherId int primary key identity(1,1) not null,
	Name varchar(50) null,
	DOB date null,
	Gender varchar(50) null,
	Mobile bigint null,
	Email varchar(100) null,
	Address varchar(max) null,
	Password varchar(100) null
)

create table TeacherSubject(
	Id int primary key identity(1,1) not null,
	ClassId int foreign key references Class (ClassId) null,
	SubjectId int foreign key references Subject (SubjectId) null,
	TeacherId int foreign key references Teacher (TeacherId) null,
)

create table TeacherAttendance(
	Id int primary key identity(1,1) not null,
	TeacherId int foreign key references Teacher (TeacherId) null,
	Status bit null,
	Date date null,
)

create table StudentAttendance(
	Id int primary key identity(1,1) not null,
	ClassId int foreign key references Class (ClassId) null,
	SubjectId int foreign key references Subject (SubjectId) null,
	RollNo varchar(50) null,
	Status bit null,
	Date date null,
)

create table Fees(
	FeeId int primary key identity(1,1) not null,
	ClassId int foreign key references Class (ClassId) null,
	FeeAmount int null
)

create table Exam(
	ExamId int primary key identity(1,1) not null,
	ClassId int foreign key references Class (ClassId) null,
	SubjectId int foreign key references Subject (SubjectId) null,
	RollNo varchar(50) null,
	TotalMarks int null,
	OutOfMarks int null
)

Create table Expense(
	ExpenseId int primary key identity(1,1) not null,
	ClassId int foreign key references Class (ClassId) null,
	SubjectId int foreign key references Subject (SubjectId) null,
	ChargeAmount int null
)

Select Row_NUMBER() over(Order by (Select 1)) as [Sr.No], ClassId, ClassName from Class

select * from Class

delete from Fees

select ROW_NUMBER() over(order by (select 1)) as [Sr.No], ts.Id, ts.ClassId, c.ClassName, ts.SubjectId,
s.SubjectName, ts.TeacherId, t.Name from TeacherSubject ts inner join Class c on ts.ClassId = c.ClassId
inner join Subject s on ts.SubjectId = s.SubjectId inner join Teacher t on ts.TeacherId = t.TeacherId

select ts.Id, ts.ClassId, ts.SubjectId, s.SubjectName from TeacherSubject ts inner join Subject s on ts.SubjectId = s.SubjectId where ts.Id = 1

select ROW_NUMBER() over(order by (select 1)) as [Sr.No], e.ExpenseId, e.ClassId, c.ClassName, e.SubjectId, s.SubjectName, e.ChargeAmount from Expense
e inner join Class c on e.ClassId = c.ClassId inner join Subject s on e.SubjectId = s.SubjectId

select e.ExpenseId, e.ClassId, e.SubjectId, s.SubjectName from Expense e inner join Subject s on e.SubjectId = s.SubjectId where e.ExpenseId = '1'

select ROW_NUMBER() over(order by (select 1)) as [Sr.No], s.StudentId, s.[Name], s.DOB, s.Gender, s.Mobile, s.RollNo, s.[Address], c.ClassId, c.ClassName 
from Student s inner join Class c on c.ClassId = s.ClassId

select ROW_NUMBER() over(order by (select 1)) as [Sr.No], e.ExamId, e.ClassId, c.ClassName, e.SubjectId, s.SubjectName, e.RollNo, e.TotalMarks, e.OutOfMarks 
from Exam e inner join Class c on e.ClassId = c.ClassId inner join Subject s on e.SubjectId = s.SubjectId

select ROW_NUMBER() over(order by (select 1)) as [Sr.No], e.ExamId, e.ClassId, c.ClassName, e.SubjectId, s.SubjectName, e.RollNo, e.TotalMarks, e.OutOfMarks from Exam e inner join Class c on c.ClassId = e.ClassId inner join Subject s on s.SubjectId = e.SubjectId

select TeacherId, Name, Mobile, Email from Teacher