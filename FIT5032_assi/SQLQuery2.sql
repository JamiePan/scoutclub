MERGE INTO Activity AS Target 
USING (VALUES 
        (1, 'Running', 1), 
        (2, 'Climbing', 1), 
        (3, 'Cycling', 2)
) 
AS Source (ActivityID, Description, AddressID) 
ON Target.ActivityID = Source.ActivityID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Description, AddressID) 
VALUES (Description, AddressID);

MERGE INTO Staff AS Target
USING (VALUES 
        (1, 'Tibbetts', 'Donnie', '2013-09-01'), 
        (2, 'Guzman', 'Liza', '2012-01-13'), 
(3, 'Catlett', 'Phil', '2011-09-03')
)
AS Source (StaffID, LastName, FirstName, EmployedDate)
ON Target.StaffID = Source.StaffID
WHEN NOT MATCHED BY TARGET THEN
INSERT (LastName, FirstName, EmployedDate)
VALUES (LastName, FirstName, EmployedDate);

MERGE INTO ClassArrangement AS Target
USING (VALUES 
(1, 20, 1, 1),
(2, 20, 1, 2),
(3, 10, 2, 3),
(4, 10, 2, 1),
(5, 15, 3, 1),
(6, 15, 3, 2)
)
AS Source (ClassID, Available, ActivityID, StaffID)
ON Target.ClassID = Source.ClassID
WHEN NOT MATCHED BY TARGET THEN
INSERT (Available, ActivityID, StaffID)
VALUES (Available, ActivityID, StaffID);