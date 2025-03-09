use bank
go

delete from Person
go

delete from City
go

delete from Credit
go

delete from CreditInfo
go

delete from Deposit
go

delete from DepositInfo
go

delete from [Transaction]
go

delete from Account
go

delete from AccountType
go

Use Bank
go

Insert into City (Id, Name)
Values
('bab30eb7-4316-e811-9c77-1c1b0d1151db', '�����'),
('bbb30eb7-4316-e811-9c77-1c1b0d1151db', '������'),
('bcb30eb7-4316-e811-9c77-1c1b0d1151db', '������'),
('bdb30eb7-4316-e811-9c77-1c1b0d1151db', '�������'),
('beb30eb7-4316-e811-9c77-1c1b0d1151db', '�����')
go


use Bank
go


--delete from Person

Insert into Person(Id, FirstName, LastName, Patronymic, DateOfBirth, SeriaPassport, PassportNumber, KemVidan,
                    DateVidan, IdentNumber, BirthPlace, CityId, Address, HomePhone, MobPhone, Email, WorkPlace,
                    Position, FamilyStatus, Nationality, Disability, Pensioner, Salary, Army)

Values
('bab30eb7-1111-e811-9c77-1c1b0d1151db', '�����', '�������', '����������', '27-06-2000', 'AB', '1112223', '������ ����',
'10-10-2020', '5555556A777PB8', '�����', 'bab30eb7-4316-e811-9c77-1c1b0d1151db', '��. ���������, �.7', '2344321', '+375441238907', 'email@gmail.com', '�����'
, '�������', 1, 0, 0, 0, 110.10, 1),

('bab30eb7-1112-e811-9c77-1c1b0d1151db', '�������', '�������', '��������', '12-09-1959', 'AB', '9999999', '������������ ����',
'12-08-2010', '2222222B789PB1', '��, �. ������', 'bbb30eb7-4316-e811-9c77-1c1b0d1151db', '��. �������, �.10', '634547', '+375296748981', 'alex@gmail.com', '�� ������'
, '�������', 0, 0, 0, 1, 2010.90, 0),
('bab30eb7-1113-e811-9c77-1c1b0d1151db', '������', '������', '��������', '28-01-1979', 'AB', '6272727', '���������� ����',
'18-10-2017', '3333333C000PB2', '��, �. ������', 'beb30eb7-4316-e811-9c77-1c1b0d1151db', '��. ������������ �.89, ��.190', null, null, null, '�� �����'
, null, 0, 0, 0, 0, 3000, 1)





use Bank
go


DBCC CHECKIDENT ('[AccountType]', RESEED, 0);
GO

insert into AccountType (Name)
values
('Cashier'),
('Fond'),
('Client')



Insert into Account (Id, PersonId, AccountTypeId, Number, Passive, Currency, Deleted, Debet, Credit)
values
('a3e0c377-6359-4744-a297-9977ed2353b1', null, 1, '101000011001', 0, 0, 0, 0, 0),
('a3e0c377-6359-4744-a297-9977ed2353b2', null, 2, '732700011002', 1, 0, 0, 0, 100000000000)


DBCC CHECKIDENT ('[DepositInfo]', RESEED, 0);
GO

insert into DepositInfo(Name, Persent, Currency, Duration, Revocable, MinSum)
values
('"����i ����������� BYN"', 20, 0, 120, 0, 1000),
('"����i ����������� USD"', 2, 3, 120, 0, 100),
('"�� ���� �������� BYN"', 10, 0, 90, 1, 2000),
('"�� ���� �������� USD"', 1, 3, 90, 1, 200)


DBCC CHECKIDENT ('[CreditInfo]', RESEED, 0);
GO

insert into CreditInfo(Name, Persent, Currency, Duration, Differantal, MinSum)
values
('"��������������� ������ �� ������ BYN"', 10, 0, 6, 0, 1000),
--('"����i ����������� USD"', 2, 3, 120, 0, 100),
('"���������� BYN"', 10, 0, 6, 1, 1000)
--,('"�� ���� �������� USD"', 1, 3, 90, 1, 200)