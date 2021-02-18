create database db_QUIZ
use db_QUIZ

create table TBL_UserType
(
	typeId int identity primary key,
	typeName nvarchar(100) NOT NULL
)

create table TBL_User
(
	userId bigint identity primary key,
	userName nvarchar(100) NOT NULL,
	userLoginName nvarchar(100) NOT NULL unique,
	userPassword nvarchar(100) NOT NULL,
	userImage nvarchar(50) default 'NoImage.png',
	userType int foreign key references TBL_UserType(typeId),
	userCreatedBy int
)

create table TBL_Exam
(
	examId bigint identity primary key,
	examName nvarchar(100) NOT NULL,
	examCreatedBy bigint foreign key references TBL_User(userId),
	examAppearCode nvarchar(50)
)

create table TBL_Question
(
	questionId bigint identity primary key,
	questionExamId bigint foreign key references TBL_Exam(examId),
	questionText nvarchar(max) NOT NULL,
	questionOption1 nvarchar(max) NOT NULL,
	questionOption2 nvarchar(max) NOT NULL,
	questionOption3 nvarchar(max) NOT NULL,
	questionOption4 nvarchar(max) NOT NULL,
	questionAnswer nvarchar(max) NOT NULL,
	questionType int 
)