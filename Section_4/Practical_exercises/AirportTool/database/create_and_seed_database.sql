
-- create database

USE master;
GO

CREATE DATABASE	AirportManagementDb;
GO

USE AirportManagementDb;
GO


-- create tables

CREATE TABLE Airline(
AirlineId INT IDENTITY(1,1) PRIMARY KEY,
IATACode NCHAR(2) NOT NULL,
Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Airport(
AirportId INT IDENTITY(1,1) PRIMARY KEY,
IATACode NCHAR(3) NOT NULL,
Name NVARCHAR(120) NOT NULL,
City NVARCHAR(80),
Country NVARCHAR(80),
TimeZone NVARCHAR(64) NOT NULL
);

CREATE TABLE Gate(
GateId INT IDENTITY(1,1) PRIMARY KEY,
AirportId INT NOT NULL,
Code NVARCHAR(10) NOT NULL
);

CREATE TABLE Aircraft(
AircraftId INT IDENTITY(1,1) PRIMARY KEY,
TailNumber NVARCHAR(10) NOT NULL,
Model NVARCHAR(60) NOT NULL,
SeatCapacity INT NOT NULL,
OwnedByAirlineId INT
);

CREATE TABLE Flight(
FlightId INT IDENTITY(1,1) PRIMARY KEY,
AirlineId INT NOT NULL,
FlightNumber NVARCHAR(8) NOT NULL,
OriginAirportId INT NOT NULL,
DestinationAirportId INT NOT NULL,
DefaultAircraftId INT,
IsActive BIT NOT NULL
);

CREATE TABLE FlightSchedule(
FlightScheduleId INT IDENTITY(1,1) PRIMARY KEY,
FlightId INT NOT NULL,
ScheduledDepartureUtc DATETIME2 NOT NULL,
ScheduledArrivalUtc DATETIME2 NOT NULL,
GateId INT,
AssignedAircraftId INT,
Status TINYINT NOT NULL
);

CREATE TABLE Ticket(
TicketId BIGINT IDENTITY(1,1) PRIMARY KEY,
FlightScheduleId INT NOT NULL,
FareClass NVARCHAR(2) NOT NULL,
BasePrice DECIMAL(10,2) NOT NULL,
Taxes DECIMAL(10,2) NOT NULL,
TotalPrice DECIMAL(10,2) NOT NULL,
Currency NCHAR(3) NOT NULL,
IsRefundable BIT NOT NULL,
SeatInventory INT NOT NULL
);

CREATE TABLE Booking(
BookingId BIGINT IDENTITY(1,1) PRIMARY KEY,
TicketId BIGINT NOT NULL,
PassengerFullName NVARCHAR(120) NOT NULL,
PassengerEmail NVARCHAR(120) NOT NULL,
ConfirmationCode NVARCHAR(8) NOT NULL,
Quantity INT NOT NULL,
Status TINYINT NOT NULL,
CreatedUtc DATETIME2 NOT NULL
);


-- foreign keys

ALTER TABLE Gate
ADD CONSTRAINT FK_Gate_Airport
	FOREIGN KEY (AirportId) REFERENCES Airport(AirportId);

ALTER TABLE Aircraft
ADD CONSTRAINT FK_Aircraft_Airline
	FOREIGN KEY (OwnedByAirlineId) REFERENCES Airline (AirlineId);

ALTER TABLE Flight
ADD 
	CONSTRAINT FK_Flight_Airline
		FOREIGN KEY (AirlineId) REFERENCES Airline (AirlineId),
	CONSTRAINT FK_Flight_OriginAirport
		FOREIGN KEY (OriginAirportId) REFERENCES Airport (AirportId),
	CONSTRAINT FK_Flight_DestinationAirport
		FOREIGN KEY (DestinationAirportId) REFERENCES Airport (AirportId),
	CONSTRAINT FK_Flight_Aircraft
		FOREIGN KEY (DefaultAircraftId) REFERENCES Aircraft (AircraftId);

ALTER TABLE FlightSchedule
ADD 
	CONSTRAINT FK_FlightSchedule_Flight
		FOREIGN KEY (FlightId) REFERENCES Flight (FlightId),
	CONSTRAINT FK_FlightSchedule_Gate
		FOREIGN KEY (GateId) REFERENCES Gate (GateId),
	CONSTRAINT FK_FlightSchedule_Aircraft
		FOREIGN KEY (AssignedAircraftId) REFERENCES Aircraft (AircraftId);

ALTER TABLE Ticket
ADD CONSTRAINT FK_Ticket_FlightSchedule
		FOREIGN KEY (FlightScheduleId) REFERENCES FlightSchedule (FlightScheduleId);

ALTER TABLE Booking
ADD CONSTRAINT FK_Booking_Ticket
		FOREIGN KEY (TicketId) REFERENCES Ticket (TicketId);


-- unique constraints

ALTER TABLE Airline
ADD CONSTRAINT UQ_Airline_IATACode
	UNIQUE (IATACode);

ALTER TABLE Airport
ADD CONSTRAINT UQ_Airport_IATACode
	UNIQUE (IATACode);

ALTER TABLE Gate
ADD CONSTRAINT UQ_Gate_AirportId_Code
	UNIQUE (AirportId, Code);

ALTER TABLE Aircraft
ADD CONSTRAINT UQ_Aircraft_TailNumber
	UNIQUE (TailNumber);

ALTER TABLE Booking
ADD CONSTRAINT UQ_Booking_ConfirmationCode
	UNIQUE (ConfirmationCode);


-- check constraints

ALTER TABLE Aircraft
ADD CONSTRAINT CHK_Aircraft_SeatCapacity_GreaterThan0
	CHECK (SeatCapacity > 0);

ALTER TABLE Flight
ADD CONSTRAINT CHK_Flight_DifferentOriginAndDestination
	CHECK (OriginAirportId <> DestinationAirportId)

ALTER TABLE FlightSchedule
ADD CONSTRAINT CHK_FlightSchedule_Status_ValidValue
    CHECK (Status BETWEEN 0 AND 4);

ALTER TABLE FlightSchedule
ADD CONSTRAINT CHK_FlightSchedule_DepartureBeforeArrival
    CHECK (ScheduledDepartureUtc < ScheduledArrivalUtc);

ALTER TABLE Ticket
ADD CONSTRAINT CHK_Ticket_FareClass_ValidValue
    CHECK (FareClass IN ('Y', 'M', 'J', 'F'));

ALTER TABLE Ticket
ADD CONSTRAINT CHK_Ticket_BasePrice_GreaterOrEqualTo0
    CHECK (BasePrice >= 0);

ALTER TABLE Ticket
ADD CONSTRAINT CHK_Ticket_Taxes_GreaterOrEqualTo0
    CHECK (Taxes >= 0);

ALTER TABLE Ticket
ADD CONSTRAINT CHK_Ticket_TotalPrice_ValidValue
    CHECK (TotalPrice = BasePrice + Taxes);

ALTER TABLE Ticket
ADD CONSTRAINT CHK_Ticket_SeatInventory_GreaterOrEqualTo0
    CHECK (SeatInventory >= 0);

ALTER TABLE Booking
ADD CONSTRAINT CHK_Booking_Quantity_GreaterThan0
    CHECK (Quantity > 0);

ALTER TABLE Booking
ADD CONSTRAINT CHK_Booking_Status_ValidValue
    CHECK (Status BETWEEN 0 AND 1);

-- default constraints

ALTER TABLE Flight
ADD CONSTRAINT DF_Flight_IsActive
    DEFAULT 1 FOR IsActive;

ALTER TABLE Ticket
ADD CONSTRAINT DF_Ticket_IsRefundable
    DEFAULT 0 FOR IsRefundable;

ALTER TABLE Booking
ADD CONSTRAINT DF_Booking_CreatedUtc
    DEFAULT GetUtcDate() FOR CreatedUtc;


-- nonclustered indexes

CREATE INDEX IX_Flight_AirlineId_FlightNumber
    ON Flight (AirlineId, FlightNumber)
	WHERE IsActive = 1;

CREATE INDEX IX_Flight_OriginAirportId_DestinationAirportId
    ON Flight (OriginAirportId, DestinationAirportId);

CREATE INDEX IX_FlightSchedule_FlightId_ScheduledDepartureUtc
    ON FlightSchedule (FlightId, ScheduledDepartureUtc);

CREATE INDEX IX_Ticket_FlightId_FareClass
    ON Ticket (FlightScheduleId, FareClass);


-- seed data

INSERT INTO Airline (IATACode, Name)
VALUES
    ('RO', 'TAROM'),
    ('LH', 'Lufthansa'),
    ('BA', 'British Airways');

INSERT INTO Airport (IATACode, Name, City, Country, TimeZone)
VALUES
    ('OTP', 'Henri Coanda International Airport', 'Bucharest', 'Romania', 'Europe/Bucharest'),
    ('LHR', 'London Heathrow Airport', 'London', 'United Kingdom', 'Europe/London'),
    ('FRA', 'Frankfurt Airport', 'Frankfurt', 'Germany', 'Europe/Berlin');

INSERT INTO Aircraft (TailNumber, Model, SeatCapacity, OwnedByAirlineId)
VALUES
    ('YR-BGA', 'Boeing 737-700', 149, 1),  -- owned by TAROM
    ('D-AIBC', 'Airbus A320-200', 168, 2), -- owned by Lufthansa
    ('G-EUPJ', 'Airbus A319-100', 144, 3); -- owned by British Airways

INSERT INTO Gate (AirportId, Code)
VALUES
    (1, 'A1'), -- OTP
    (2, 'B2'), -- LHR
    (3, 'C3'); -- FRA

INSERT INTO Flight (
    AirlineId,
    FlightNumber,
    OriginAirportId,
    DestinationAirportId,
    DefaultAircraftId,
    IsActive
)
VALUES
    (1, 'RO101', 1, 2, 1, 1), -- OTP ? LHR (TAROM)
    (2, 'LH202', 3, 2, 2, 1), -- FRA ? LHR (Lufthansa)
    (3, 'BA303', 2, 1, 3, 1); -- LHR ? OTP (British Airways)

INSERT INTO FlightSchedule (
    FlightId,
    ScheduledDepartureUtc,
    ScheduledArrivalUtc,
    GateId,
    AssignedAircraftId,
    Status
)
VALUES
    (1, '2025-02-01T06:00:00Z', '2025-02-01T08:30:00Z', 1, 1, 1),
    (2, '2025-02-02T09:00:00Z', '2025-02-02T10:30:00Z', 3, 2, 1),
    (3, '2025-02-03T15:00:00Z', '2025-02-03T17:45:00Z', 2, 3, 1);

    INSERT INTO Ticket (
    FlightScheduleId,
    FareClass,
    BasePrice,
    Taxes,
    TotalPrice,
    Currency,
    IsRefundable,
    SeatInventory
)
VALUES
    (1, 'Y', 180.00, 40.00, 220.00, 'EUR', 0, 120),
    (2, 'M', 150.00, 30.00, 180.00, 'EUR', 0, 100),
    (3, 'J', 450.00, 90.00, 540.00, 'EUR', 1, 40);

INSERT INTO Booking (
    TicketId,
    PassengerFullName,
    PassengerEmail,
    ConfirmationCode,
    Quantity,
    Status
)
VALUES
    (1, 'Andrei Popescu', 'andrei.popescu@email.ro', 'ROA12345', 1, 1),
    (2, 'Anna Müller', 'anna.mueller@email.de', 'LHB67890', 2, 1),
    (3, 'James Smith', 'james.smith@email.co.uk', 'BAC24680', 1, 0);


-- seeding for upcoming scheduled flights

DECLARE @Base DATETIME2 = DATEADD(DAY, 1, SYSUTCDATETIME());

INSERT INTO FlightSchedule (
    FlightId,
    ScheduledDepartureUtc,
    ScheduledArrivalUtc,
    GateId,
    AssignedAircraftId,
    Status
)
VALUES
-- FlightId = 1
(
    1,
    DATEADD(HOUR, 6, @Base),
    DATEADD(HOUR, 8, @Base),
    1,
    1,
    1
),

-- FlightId = 2
(
    2,
    DATEADD(DAY, 1, DATEADD(HOUR, 9, @Base)),
    DATEADD(DAY, 1, DATEADD(HOUR, 11, @Base)),
    3,
    2,
    1
),

-- FlightId = 3
(
    3,
    DATEADD(DAY, 2, DATEADD(HOUR, 15, @Base)),
    DATEADD(DAY, 2, DATEADD(HOUR, 18, @Base)),
    2,
    3,
    1
);


-- rollback database

--USE master;
--GO

--IF DB_ID('AirportManagementDb') IS NOT NULL
--BEGIN
--    ALTER DATABASE AirportManagementDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--    DROP DATABASE AirportManagementDb;
--END
--GO


