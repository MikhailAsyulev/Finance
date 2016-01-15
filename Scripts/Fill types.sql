

INSERT INTO DepositType (Name, ReturnTerm, [Percent], IsActive, Description, CurrencyShort)
VALUES 
	('��������������', 315360000000000 * 3, 0.29, 1, '�������������� ����� ������ �� 3 ���� � ����������� ������. ���������� ������ - 29% �������.', 'BYR'),
	('��������������', 315360000000000 * 3, 0.04, 1, '�������������� ����� ������ �� 3 ���� � ������������ ��������. ���������� ������ - 4% �������.', 'USD'),
	('�� �������������', 315360000000000 * 25, 0.005, 1, '����� � ����������� ������. ���������� ������ - 0.5% �������.', 'BYR')

INSERT INTO CreditType (Name, [Percent], OverduePercent, ReturnTerm, IsActive, Description, CurrencyShort)
VALUES
	('�� ������������ ����������', 0.42, 0.5, 315360000000000 * 7, 1, '������ �� ������� ���������� ������ �� 7 ��� � ����������� ������. ����� ������� � �������� ������������������ ���������. ���������� ������ - 42% �������', 'BYR'),
	('���������������', 0.4, 0.5, 315360000000000 * 2, 1, '������ ������ �� 2 ���� � ����������� ������. ����� ������� � �������� ������������������ ���������. ���������� ������ - 40% �������', 'BYR')
