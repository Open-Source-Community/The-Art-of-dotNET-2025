-- Insert Users
INSERT INTO Users (Name, Email, Password)
VALUES
    ('Admin', 'admin@example.com', '$2a$12$N9qo8uLOickgx2ZMRZoMy.MH9J3mY7.F6zD3E6KQZ7Qq9nT7XQyHO', 1),
    ('John Doe', 'john.doe@example.com', '$2a$12$sT7WJQWl4kU3.9g5Vp8rB.Xr7cY1Zb0jKfLmN2vP3qR5tT6yH1D2C', 0),
    ('Jane Smith', 'jane.smith@example.com', '$2a$12$sT7WJQWl4kU3.9g5Vp8rB.Xr7cY1Zb0jKfLmN2vP3qR5tT6yH1D2C', 0);

-- Insert Tasks for John Doe (User 1)
INSERT INTO Tasks (Title, Score, Body, IsDone, CreatorId)
VALUES
    ('Complete Project Proposal', 10, 'Write and submit the project proposal.', 0, 1),
    ('Set Up Database', 8, 'Set up the database using EF Core.', 1, 1),
    ('Design UI', 7, 'Design the user interface for the task manager.', 0, 1),
    ('Write Unit Tests', 9, 'Write unit tests for the back-end API.', 0, 1),
    ('Deploy Application', 6, 'Deploy the application to Azure.', 0, 1);

-- Insert Tasks for Jane Smith (User 2)
INSERT INTO Tasks (Title, Score, Body, IsDone, CreatorId)
VALUES
    ('Prepare Presentation', 8, 'Prepare a presentation for the team meeting.', 0, 2),
    ('Review Code', 7, 'Review the code for the task manager project.', 1, 2),
    ('Update Documentation', 6, 'Update the project documentation.', 0, 2),
    ('Fix Bugs', 9, 'Fix bugs reported by the QA team.', 0, 2),
    ('Plan Sprint', 7, 'Plan the next sprint for the development team.', 0, 2);