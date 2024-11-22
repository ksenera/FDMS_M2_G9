
CREATE TABLE AttitudeData (
    AircraftID NVARCHAR(50) PRIMARY KEY,
    Timestamp DATETIME,
    Altitude FLOAT,
    Pitch FLOAT,
    Bank FLOAT,
    Checksum INT
);