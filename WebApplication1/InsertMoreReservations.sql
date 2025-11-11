-- Insert 10 more sample reservation data
USE ELDNET;

INSERT INTO Reservations (Organization, ActTitle, Venue, DateNeeded, TimeFrom, TimeTo, Participants, Date, Speaker, PurposeObjective, EquipmentFacilities, NatureOfActivity, SourceOfFunds, Approved, Approver, ApprovalDate)
VALUES 
('Photography Club', 'Photo Exhibition', 'Gallery Hall', '2025-11-26', '2025-11-26 10:00:00', '2025-11-26 18:00:00', 'photographers,visitors,judges', '2025-11-12', 'Ms. Claire Anderson', 'Showcase student photography work and talent', 'Display Boards, Lighting, Easels', 'Exhibition', 'Arts Fund', 1, 'admin', '2025-11-12'),

('IT Society', 'Hackathon 2025', 'Computer Lab 3', '2025-12-05', '2025-12-05 08:00:00', '2025-12-05 20:00:00', 'developers,mentors,sponsors', '2025-11-12', 'Engr. Mark Stevens', '24-hour coding competition for innovation', 'Computers, WiFi, Projector, Snacks', 'Competition', 'Tech Sponsors', NULL, NULL, NULL),

('History Department', 'Historical Documentary Screening', 'Auditorium B', '2025-11-29', '2025-11-29 15:00:00', '2025-11-29 17:30:00', 'students,faculty,history enthusiasts', '2025-11-12', 'Prof. Richard Martinez', 'Educational screening about Philippine history', 'Projector, Sound System, Screen', 'Educational', 'History Department', 1, 'shewakram', '2025-11-12'),

('Medical Students Association', 'First Aid Training', 'Nursing Lab', '2025-11-27', '2025-11-27 13:00:00', '2025-11-27 16:00:00', 'medical students,volunteers', '2025-11-12', 'Dr. Patricia Chen', 'Train students in basic life support and first aid', 'Medical Equipment, Mannequins, Training Kit', 'Training', 'Medical Department', NULL, NULL, NULL),

('Literature Club', 'Poetry Reading Night', 'Library Reading Room', '2025-12-03', '2025-12-03 18:00:00', '2025-12-03 20:00:00', 'poets,writers,literature lovers', '2025-11-12', 'Prof. Amanda Wright', 'Evening of poetry recitation and creative writing', 'Microphone, Chairs, Refreshments', 'Cultural', 'Literature Fund', 0, 'admin', '2025-11-12'),

('Sports Committee', 'Basketball Tournament Finals', 'Gymnasium', '2025-12-08', '2025-12-08 14:00:00', '2025-12-08 18:00:00', 'teams,coaches,audience', '2025-11-12', 'Coach Daniel Rodriguez', 'Championship game of intramural basketball', 'Scoreboard, Timer, Sound System', 'Sports', 'Athletics Budget', 1, 'admin', '2025-11-12'),

('Psychology Department', 'Mental Health Awareness Seminar', 'Conference Room B', '2025-12-02', '2025-12-02 09:00:00', '2025-12-02 12:00:00', 'students,counselors,faculty', '2025-11-12', 'Dr. Lisa Thompson', 'Raise awareness about student mental health', 'Projector, Microphone, Handouts', 'Seminar', 'Wellness Program', NULL, NULL, NULL),

('Culinary Club', 'Cooking Competition', 'Home Economics Kitchen', '2025-12-06', '2025-12-06 10:00:00', '2025-12-06 15:00:00', 'contestants,judges,audience', '2025-11-12', 'Chef Roberto Santos', 'Student chef competition with live cooking', 'Stoves, Ingredients, Utensils, Tables', 'Competition', 'Culinary Fund', 1, 'shewakram', '2025-11-12'),

('Dance Troupe', 'Modern Dance Workshop', 'Dance Studio', '2025-11-23', '2025-11-23 16:00:00', '2025-11-23 19:00:00', 'dancers,choreographers', '2025-11-12', 'Ms. Grace Martinez', 'Workshop on contemporary dance techniques', 'Sound System, Mirrors, Mats', 'Workshop', 'Arts Department', NULL, NULL, NULL),

('Volunteer Corps', 'Community Outreach Planning', 'Meeting Room 2', '2025-11-21', '2025-11-21 17:00:00', '2025-11-21 19:00:00', 'volunteers,organizers', '2025-11-12', 'Mr. James Williams', 'Plan upcoming community service activities', 'Whiteboard, Markers, Projector', 'Planning', 'Community Service Fund', 0, 'admin', '2025-11-12');

SELECT 'Successfully inserted ' + CAST(@@ROWCOUNT AS VARCHAR) + ' more reservation records.' AS Result;