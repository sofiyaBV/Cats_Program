Create database Cats
go use Cats

Create table [User]
(
[Id] int identity primary key,
[Login] nvarchar(50),
[Password] nvarchar(max)
)

Create table [SaveImage]
(
[Id] int identity primary key,
[Facts] nvarchar(max),
[Image] varbinary(max)
)

Create table [Account]
(
[id] int identity primary key,
[UserId] int not null foreign key references [User](Id),
[SaveImageId] int not null foreign key references [SaveImage](Id),
)

INSERT INTO [User] ([Login], [Password])
VALUES
    ('user1', 'password1'),
    ('user2', 'password2'),
    ('user3', 'password3');

-- Вставка записей в таблицу [SaveImage]
INSERT INTO [SaveImage] ([Facts], [Image])
VALUES
    ('Facts about image 1', 0x012345),
    ('Facts about image 2', 0x6789AB),
    ('Facts about image 3', 0xCDEF01);

-- Вставка записей в таблицу [Account]
INSERT INTO [Account] ([UserId], [SaveImageId])
VALUES
    (1, 1),
    (2, 2),
    (3, 3);