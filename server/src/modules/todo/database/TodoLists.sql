CREATE TABLE TodoLists
(
    Id INT PRIMARY KEY NOT NULL,
    OwnerId INT NOT NULL,
    Name VARCHAR(64) NOT NULL,
    Items VARCHAR(MAX) NOT NULL,
    Version ROWVERSION NOT NULL
)