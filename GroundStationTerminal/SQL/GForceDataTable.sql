
CREATE TABLE GForceData (
    AircraftID NVARCHAR(50) PRIMARY KEY,
    Timestamp DATETIME,
    AccelX FLOAT,
    AccelY FLOAT,
    AccelZ FLOAT,
    Weight FLOAT,
    Checksum INT
);