-- Insert sample reservation data
USE ELDNET;

INSERT INTO Reservations (Organization, ActTitle, Venue, DateNeeded, TimeFrom, TimeTo, Participants, Date, Speaker, PurposeObjective, EquipmentFacilities, NatureOfActivity, SourceOfFunds, Approved, Approver, ApprovalDate)
VALUES 
('Computer Science Society', 'Coding Workshop', 'Room 301', '2025-11-20', '2025-11-20 09:00:00', '2025-11-20 12:00:00', 'blue,ivan,richlan,teves', '2025-11-12', 'Dr. John Smith', 'To teach students advanced programming concepts', 'Projector, Whiteboard, Laptops', 'Educational', 'Department Budget', NULL, NULL, NULL),

('Business Club', 'Entrepreneurship Seminar', 'Auditorium', '2025-11-25', '2025-11-25 14:00:00', '2025-11-25 17:00:00', 'mjhblqyfc', '2025-11-12', 'Ms. Sarah Johnson', 'To inspire students about business opportunities', 'Sound System, Projector, Stage', 'Seminar', 'Student Council', 1, 'admin', '2025-11-12'),

('Engineering Society', 'Robotics Competition', 'Laboratory 5', '2025-11-28', '2025-11-28 08:00:00', '2025-11-28 18:00:00', 'team1,team2,team3,team4', '2025-11-12', 'Eng. Michael Brown', 'Annual robotics challenge for engineering students', 'Tables, Power Supply, Testing Equipment', 'Competition', 'Sponsors', NULL, NULL, NULL),

('Arts Department', 'Theater Performance', 'Theater Hall', '2025-12-01', '2025-12-01 19:00:00', '2025-12-01 21:30:00', 'cast,crew,volunteers', '2025-11-12', 'Prof. Emily Davis', 'End of semester theater showcase', 'Lighting, Sound System, Costumes', 'Cultural', 'Arts Budget', 1, 'shewakram', '2025-11-12'),

('Science Club', 'Chemistry Experiment Day', 'Science Lab 2', '2025-11-22', '2025-11-22 10:00:00', '2025-11-22 15:00:00', 'students,faculty', '2025-11-12', 'Dr. Robert Wilson', 'Hands-on chemistry experiments for interested students', 'Lab Equipment, Safety Gear, Chemicals', 'Educational', 'Science Department', 0, 'admin', '2025-11-12'),

('Music Society', 'Band Practice', 'Music Room', '2025-11-18', '2025-11-18 16:00:00', '2025-11-18 19:00:00', 'band members', '2025-11-12', 'Mr. David Lee', 'Preparation for upcoming concert', 'Instruments, Amplifiers, Microphones', 'Practice', 'Music Fund', NULL, NULL, NULL),

('Debate Team', 'Inter-School Debate', 'Conference Room A', '2025-11-30', '2025-11-30 13:00:00', '2025-11-30 16:00:00', 'debaters,judges,audience', '2025-11-12', 'Prof. Jennifer Garcia', 'Competitive debate event with other universities', 'Microphones, Timer, Podiums', 'Competition', 'Student Activities', 1, 'admin', '2025-11-12'),

('Environmental Club', 'Tree Planting Activity', 'Campus Grounds', '2025-11-24', '2025-11-24 07:00:00', '2025-11-24 11:00:00', 'volunteers,students,faculty', '2025-11-12', 'Dr. Maria Santos', 'Environmental awareness and campus beautification', 'Shovels, Seedlings, Water', 'Outreach', 'Environmental Fund', NULL, NULL, NULL);

SELECT 'Successfully inserted ' + CAST(@@ROWCOUNT AS VARCHAR) + ' reservation records.' AS Result;