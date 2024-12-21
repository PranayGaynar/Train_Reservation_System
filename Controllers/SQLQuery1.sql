use [Db_TrainReservation]

CREATE TABLE Train (
    TrainId INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing primary key
    TrainName NVARCHAR(100) NOT NULL,      -- Train name, required field
    TrainNumber NVARCHAR(20) NOT NULL,    -- Train number, required field
    TrainType NVARCHAR(20) NOT NULL       -- Enum values stored as strings
);

CREATE TABLE TrainRoute (
    TrainRouteId INT IDENTITY(1,1) PRIMARY KEY,
    TrainId INT NOT NULL, -- Foreign key referencing Train table
    SourceStation NVARCHAR(100) NOT NULL,
    DestinationStation NVARCHAR(100) NOT NULL,
    DepartureTime DATETIME NOT NULL,
    ArrivalTime DATETIME NOT NULL,
    AvailableSeats INT NOT NULL,
    TotalSeats INT NOT NULL,
    FOREIGN KEY (TrainId) REFERENCES Train(TrainId) ON DELETE CASCADE
);

CREATE TABLE Booking (
    BookingId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL, -- Foreign key referencing User table
    TrainRouteId INT NOT NULL, -- Foreign key referencing TrainRoute table
    JourneyDate DATE NOT NULL,
    SeatClass NVARCHAR(50) NOT NULL, -- Enum-like (e.g., Sleeper, AC)
    PNR NVARCHAR(20) NOT NULL UNIQUE,
    Amount DECIMAL(10,2) NOT NULL,
    IsCancelled BIT NOT NULL DEFAULT 0,
    BookingDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES [Users](UserId) ON DELETE CASCADE,
    FOREIGN KEY (TrainRouteId) REFERENCES TrainRoute(TrainRouteId) ON DELETE CASCADE
);

CREATE TABLE Payment (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT NOT NULL, -- Foreign key referencing Booking table
    Amount DECIMAL(10,2) NOT NULL,
    PaymentStatus NVARCHAR(50) NOT NULL, -- Enum-like (e.g., Success, Failed, Pending)
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (BookingId) REFERENCES Booking(BookingId) ON DELETE CASCADE
);

-- Insert sample Train
INSERT INTO Train (TrainName, TrainNumber, TrainType)
VALUES ('Shatabdi Express', '12345', 'Express');

-- Insert sample TrainRoute
INSERT INTO TrainRoute (TrainId, SourceStation, DestinationStation, DepartureTime, ArrivalTime, AvailableSeats, TotalSeats)
VALUES (1, 'Delhi', 'Mumbai', '2024-12-20 06:00:00', '2024-12-20 18:00:00', 50, 100);

-- Insert sample User
INSERT INTO [Users] (Name, Email, Password, PhoneNumber, Role)
VALUES ('John Doe', 'john.doe@example.com', 'password123', '1234567890', 'Passenger');

-- Insert sample Booking
INSERT INTO Booking (UserId, TrainRouteId, JourneyDate, SeatClass, PNR, Amount, IsCancelled)
VALUES (1, 1, '2024-12-20', 'AC', 'PNR1234567890', 1500.00, 0);

-- Insert sample Payment
INSERT INTO Payment (BookingId, Amount, PaymentStatus)
VALUES (1, 1500.00, 'Success');

ALTER TABLE [Users]
ALTER COLUMN Role VARCHAR(50) NOT NULL;



