
INSERT INTO SubscriptionPlan ( Name, Price, BorrowLimit, AccessEbooks, AccessAudiobooks, CreatedDate, UpdatedDate)
VALUES 
('Free', 0.00, 3, 0, 0, '2024-12-01 00:00:00', '2024-12-01 00:00:00'),
( 'Standard', 10.00, 5, 0, 0, '2024-12-01 00:00:00', '2024-12-01 00:00:00'),
( 'Premium', 20.00, 7, 1, 1, '2024-12-01 00:00:00', '2024-12-01 00:00:00');

INSERT INTO PaymentDuration ( Duration, Multiplier, CreatedDate, UpdatedDate)
VALUES 
( 1, 1.00, '2024-12-01 00:00:00', '2024-12-01 00:00:00'), -- 1 Month
( 3, 2.80, '2024-12-01 00:00:00', '2024-12-01 00:00:00'), -- 3 Months
( 6, 5.00, '2024-12-01 00:00:00', '2024-12-01 00:00:00'), -- 6 Months
( 12, 9.00, '2024-12-01 00:00:00', '2024-12-01 00:00:00'); -- 12 Months (1 Year)


INSERT INTO EmailTemplates (Id, EmailTypes, Title, Body)
VALUES 
(	  NEWID(),  -- Generates a unique GUID
    1, 
    'Account Created Successfully',  -- Email title (subject line)
    '<html>
<head>
  <style>
    body { font-family: Arial, sans-serif; color: #333; line-height: 1.6; }
    .container { max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9; border-radius: 8px; border: 1px solid #ddd; }
    .header { background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; border-radius: 8px 8px 0 0; }
    .content { padding: 20px; background-color: #ffffff; }
    .footer { text-align: center; padding: 10px; font-size: 12px; color: #777; }
    .button { background-color: #4CAF50; color: white; padding: 10px 20px; border-radius: 5px; text-decoration: none; }
  </style>
</head>
<body>
  <div class=''container''>
    <div class=''header''>
      <h2>Account Created Successfully</h2>
    </div>
    <div class=''content''>
      <p>Dear  {Name},</p>
      <p>We are excited to inform you that your account has been successfully created in our system!</p>
      <p>We''re thrilled to have you on board. You can now log in to your account and start using all of our features.</p>
      <p>If you have any questions or need support, feel free to reach out to our support team.</p>
      <p>Welcome aboard!</p>
      <a href=''http://localhost:4200/'' class=''button''>Go to Login</a>
    </div>
    <div class=''footer''>
      <p>&copy; 2024 Your Company. All rights reserved.</p>
    </div>
  </div>
</body>
</html>'
);

INSERT INTO EmailTemplates (Id, EmailTypes, Title, Body)
VALUES 
(
    NEWID(),  -- Generates a unique GUID
   3 ,  -- Type of the email
    'PayMent Successfully',  -- Email subject
    '<html><body><h1>Welcome!</h1><p>Otp Is {Otp} .</p></body></html>'  -- Email body (HTML)
);

-- Insert 30 Audiobook records
INSERT INTO Audiobooks (ISBN, Title, Author, Genre, PublishYear, AddedDate, FilePath, CoverImagePath)
VALUES
('978-1-234567-89-0', 'Audiobook Title 1', 'Author 1', 'Fiction', 2020, GETDATE(), 'Audiobooks/a (1).mp3', 'AudiobookCovers/a (1).jpg'),
('978-1-234567-89-1', 'Audiobook Title 2', 'Author 2', 'Science', 2019, GETDATE(), 'Audiobooks/a (2).mp3', 'AudiobookCovers/a (2).jpg'),
('978-1-234567-89-2', 'Audiobook Title 3', 'Author 3', 'Fantasy', 2021, GETDATE(), 'Audiobooks/a (3).mp3', 'AudiobookCovers/a (3).jpg'),
('978-1-234567-89-3', 'Audiobook Title 4', 'Author 4', 'Non-Fiction', 2022, GETDATE(), 'Audiobooks/a (4).mp3', 'AudiobookCovers/a (4).jpg'),
('978-1-234567-89-4', 'Audiobook Title 5', 'Author 5', 'Biography', 2020, GETDATE(), 'Audiobooks/a (5).mp3', 'AudiobookCovers/a (5).jpg'),
('978-1-234567-89-5', 'Audiobook Title 6', 'Author 6', 'Drama', 2021, GETDATE(), 'Audiobooks/a (6).mp3', 'AudiobookCovers/a (6).jpg'),
('978-1-234567-89-6', 'Audiobook Title 7', 'Author 7', 'Romance', 2018, GETDATE(), 'Audiobooks/a (7).mp3', 'AudiobookCovers/a (7).jpg'),
('978-1-234567-89-7', 'Audiobook Title 8', 'Author 8', 'History', 2020, GETDATE(), 'Audiobooks/a (8).mp3', 'AudiobookCovers/a (8).jpg'),
('978-1-234567-89-8', 'Audiobook Title 9', 'Author 9', 'Thriller', 2022, GETDATE(), 'Audiobooks/a (9).mp3', 'AudiobookCovers/a (9).jpg'),
('978-1-234567-89-9', 'Audiobook Title 10', 'Author 10', 'Mystery', 2021, GETDATE(), 'Audiobooks/a (10).mp3', 'AudiobookCovers/a (10).jpg'),
('978-1-234567-90-0', 'Audiobook Title 11', 'Author 11', 'Science Fiction', 2019, GETDATE(), 'Audiobooks/a (11).mp3', 'AudiobookCovers/a (11).jpg'),
('978-1-234567-90-1', 'Audiobook Title 12', 'Author 12', 'Adventure', 2022, GETDATE(), 'Audiobooks/a (12).mp3', 'AudiobookCovers/a (12).jpg'),
('978-1-234567-90-2', 'Audiobook Title 13', 'Author 13', 'Action', 2020, GETDATE(), 'Audiobooks/a (13).mp3', 'AudiobookCovers/a (13).jpg'),
('978-1-234567-90-3', 'Audiobook Title 14', 'Author 14', 'Comedy', 2021, GETDATE(), 'Audiobooks/a (14).mp3', 'AudiobookCovers/a (14).jpg'),
('978-1-234567-90-4', 'Audiobook Title 15', 'Author 15', 'Horror', 2020, GETDATE(), 'Audiobooks/a (15).mp3', 'AudiobookCovers/a (15).jpg'),
('978-1-234567-90-5', 'Audiobook Title 16', 'Author 16', 'Fantasy', 2021, GETDATE(), 'Audiobooks/a (16).mp3', 'AudiobookCovers/a (16).jpg'),
('978-1-234567-90-6', 'Audiobook Title 17', 'Author 17', 'Fiction', 2020, GETDATE(), 'Audiobooks/a (17).mp3', 'AudiobookCovers/a (17).jpg'),
('978-1-234567-90-7', 'Audiobook Title 18', 'Author 18', 'Non-Fiction', 2019, GETDATE(), 'Audiobooks/a (18).mp3', 'AudiobookCovers/a (18).jpg'),
('978-1-234567-90-8', 'Audiobook Title 19', 'Author 19', 'Biography', 2022, GETDATE(), 'Audiobooks/a (19).mp3', 'AudiobookCovers/a (19).jpg'),
('978-1-234567-90-9', 'Audiobook Title 20', 'Author 20', 'Science', 2020, GETDATE(), 'Audiobooks/a (20).mp3', 'AudiobookCovers/a (20).jpg'),
('978-1-234567-91-0', 'Audiobook Title 21', 'Author 21', 'Drama', 2021, GETDATE(), 'Audiobooks/a (21).mp3', 'AudiobookCovers/a (21).jpg'),
('978-1-234567-91-1', 'Audiobook Title 22', 'Author 22', 'Romance', 2020, GETDATE(), 'Audiobooks/a (22).mp3', 'AudiobookCovers/a (22).jpg'),
('978-1-234567-91-2', 'Audiobook Title 23', 'Author 23', 'History', 2021, GETDATE(), 'Audiobooks/a (23).mp3', 'AudiobookCovers/a (23).jpg'),
('978-1-234567-91-3', 'Audiobook Title 24', 'Author 24', 'Mystery', 2022, GETDATE(), 'Audiobooks/a (24).mp3', 'AudiobookCovers/a (24).jpg'),
('978-1-234567-91-4', 'Audiobook Title 25', 'Author 25', 'Thriller', 2020, GETDATE(), 'Audiobooks/a (25).mp3', 'AudiobookCovers/a (25).jpg'),
('978-1-234567-91-5', 'Audiobook Title 26', 'Author 26', 'Science Fiction', 2019, GETDATE(), 'Audiobooks/a (26).mp3', 'AudiobookCovers/a (26).jpg'),
('978-1-234567-91-6', 'Audiobook Title 27', 'Author 27', 'Action', 2021, GETDATE(), 'Audiobooks/a (27).mp3', 'AudiobookCovers/a (27).jpg'),
('978-1-234567-91-7', 'Audiobook Title 28', 'Author 28', 'Adventure', 2022, GETDATE(), 'Audiobooks/a (28).mp3', 'AudiobookCovers/a (28).jpg'),
('978-1-234567-91-8', 'Audiobook Title 29', 'Author 29', 'Comedy', 2020, GETDATE(), 'Audiobooks/a (29).mp3', 'AudiobookCovers/a (29).jpg'),
('978-1-234567-91-9', 'Audiobook Title 30', 'Author 30', 'Horror', 2021, GETDATE(), 'Audiobooks/a (30).mp3', 'AudiobookCovers/a (30).jpg');


-- Insert 30 AudiobookMetadata records
INSERT INTO AudiobookMetadatas(FileFormat, FileSize, DurationInSeconds, Language, DownloadCount, PlayCount, Narrator, Publisher, Description, DigitalRights, AudiobookId)
VALUES
('MP3', 150.5, 3600, 'English', 100, 50, 'Narrator 1', 'Publisher 1', 'Description of Audiobook 1', 'DRM Free', 1),
('MP3', 180.0, 4000, 'English', 120, 60, 'Narrator 2', 'Publisher 2', 'Description of Audiobook 2', 'DRM Protected', 2),
('MP3', 200.5, 4200, 'Spanish', 80, 40, 'Narrator 3', 'Publisher 3', 'Description of Audiobook 3', 'DRM Free', 3),
('MP3', 190.0, 4500, 'English', 150, 75, 'Narrator 4', 'Publisher 4', 'Description of Audiobook 4', 'DRM Protected', 4),
('MP3', 220.0, 3800, 'English', 110, 55, 'Narrator 5', 'Publisher 5', 'Description of Audiobook 5', 'DRM Free', 5),
('MP3', 250.0, 3900, 'French', 130, 65, 'Narrator 6', 'Publisher 6', 'Description of Audiobook 6', 'DRM Free', 6),
('MP3', 160.0, 3400, 'German', 90, 45, 'Narrator 7', 'Publisher 7', 'Description of Audiobook 7', 'DRM Protected', 7),
('MP3', 210.0, 4300, 'English', 100, 50, 'Narrator 8', 'Publisher 8', 'Description of Audiobook 8', 'DRM Free', 8),
('MP3', 300.0, 5000, 'English', 200, 100, 'Narrator 9', 'Publisher 9', 'Description of Audiobook 9', 'DRM Protected', 9),
('MP3', 180.0, 3600, 'English', 110, 55, 'Narrator 10', 'Publisher 10', 'Description of Audiobook 10', 'DRM Free', 10),
('MP3', 240.0, 4000, 'Japanese', 120, 60, 'Narrator 11', 'Publisher 11', 'Description of Audiobook 11', 'DRM Protected', 11),
('MP3', 260.0, 4200, 'English', 90, 45, 'Narrator 12', 'Publisher 12', 'Description of Audiobook 12', 'DRM Free', 12),
('MP3', 200.0, 4300, 'Italian', 70, 35, 'Narrator 13', 'Publisher 13', 'Description of Audiobook 13', 'DRM Protected', 13),
('MP3', 180.0, 3600, 'English', 100, 50, 'Narrator 14', 'Publisher 14', 'Description of Audiobook 14', 'DRM Free', 14),
('MP3', 150.0, 3300, 'Spanish', 80, 40, 'Narrator 15', 'Publisher 15', 'Description of Audiobook 15', 'DRM Protected', 15),
('MP3', 220.0, 3800, 'English', 110, 55, 'Narrator 16', 'Publisher 16', 'Description of Audiobook 16', 'DRM Free', 16),
('MP3', 210.0, 4200, 'French', 120, 60, 'Narrator 17', 'Publisher 17', 'Description of Audiobook 17', 'DRM Protected', 17),
('MP3', 250.0, 4700, 'German', 130, 65, 'Narrator 18', 'Publisher 18', 'Description of Audiobook 18', 'DRM Free', 18),
('MP3', 160.0, 3500, 'English', 90, 45, 'Narrator 19', 'Publisher 19', 'Description of Audiobook 19', 'DRM Protected', 19),
('MP3', 180.0, 3600, 'Spanish', 100, 50, 'Narrator 20', 'Publisher 20', 'Description of Audiobook 20', 'DRM Free', 20),
('MP3', 220.0, 4200, 'English', 110, 55, 'Narrator 21', 'Publisher 21', 'Description of Audiobook 21', 'DRM Protected', 21),
('MP3', 200.0, 3600, 'French', 90, 45, 'Narrator 22', 'Publisher 22', 'Description of Audiobook 22', 'DRM Free', 22),
('MP3', 180.0, 3600, 'German', 100, 50, 'Narrator 23', 'Publisher 23', 'Description of Audiobook 23', 'DRM Protected', 23),
('MP3', 220.0, 4000, 'English', 110, 55, 'Narrator 24', 'Publisher 24', 'Description of Audiobook 24', 'DRM Free', 24),
('MP3', 230.0, 4600, 'Spanish', 120, 60, 'Narrator 25', 'Publisher 25', 'Description of Audiobook 25', 'DRM Protected', 25),
('MP3', 240.0, 3800, 'English', 130, 65, 'Narrator 26', 'Publisher 26', 'Description of Audiobook 26', 'DRM Free', 26),
('MP3', 260.0, 4500, 'Japanese', 140, 70, 'Narrator 27', 'Publisher 27', 'Description of Audiobook 27', 'DRM Protected', 27),
('MP3', 300.0, 4800, 'English', 150, 75, 'Narrator 28', 'Publisher 28', 'Description of Audiobook 28', 'DRM Free', 28),
('MP3', 220.0, 4200, 'French', 100, 50, 'Narrator 29', 'Publisher 29', 'Description of Audiobook 29', 'DRM Protected', 29),
('MP3', 250.0, 4600, 'German', 120, 60, 'Narrator 30', 'Publisher 30', 'Description of Audiobook 30', 'DRM Free', 30);




-- Inserting 30 sample data into Ebook table
INSERT INTO Ebooks (ISBN, Title, Author, Genre, PublishYear, AddedDate, FilePath, CoverImagePath) VALUES
('978-3-16-148410-0', ' REGULATING PULSE WIDTH MODULATORS', 'STMicroelectronics', 'electronics', 2022, '2024-01-10', 'Ebooks\sg3525.pdf', 'EbookCoverImages/sg3525.jpg'),
('978-1-86197-876-9', 'TDA2030-14 W hi-fi audio amplifier', 'Digi-Key', 'electronics', 2019, '2023-12-15', 'Ebooks\TDA2030.pdf', 'EbookCoverImages/tda2030.jpg'),
('978-0-14-044913-6', 'The Intel 8085', 'Intel Corporation', 'electronics', 2021, '2024-02-20', 'Ebooks\8085AH.pdf', 'EbookCoverImages/8085.jpg'),
('978-0-19-852663-6', 'Machine Learning Guide', 'Sarah Johnson', 'Technology', 2023, '2024-03-01', '/ebooks/ml_guide.pdf', '/images/ml_guide.jpg'),
('978-0-545-01022-1', 'Deep Dive into Python', 'David Lee', 'Programming', 2020, '2024-04-15', '/ebooks/python_dive.pdf', '/images/python_dive.jpg'),
('978-1-4028-9467-6', 'Database Design', 'Emily White', 'Education', 2018, '2024-05-05', '/ebooks/db_design.pdf', '/images/db_design.jpg'),
('978-3-642-04040-4', 'AI for Everyone', 'Chris Martin', 'Technology', 2022, '2024-06-10', '/ebooks/ai_for_everyone.pdf', '/images/ai_for_everyone.jpg'),
('978-0-262-03384-8', 'Angular Masterclass', 'Rebecca King', 'Programming', 2021, '2024-07-08', '/ebooks/angular_masterclass.pdf', '/images/angular_masterclass.jpg'),
('978-0-306-40615-7', 'React Unleashed', 'Laura Davis', 'Programming', 2022, '2024-08-18', '/ebooks/react_unleashed.pdf', '/images/react_unleashed.jpg'),
('978-1-78929-467-6', 'Mastering C#', 'Paul Walker', 'Programming', 2020, '2024-09-12', '/ebooks/mastering_csharp.pdf', '/images/mastering_csharp.jpg'),
('978-1-59059-909-1', 'Blockchain Basics', 'Alice Green', 'Technology', 2021, '2024-10-20', '/ebooks/blockchain_basics.pdf', '/images/blockchain_basics.jpg'),
('978-0-7356-6745-7', 'ASP.NET Core Essentials', 'Robert Adams', 'Programming', 2019, '2024-11-25', '/ebooks/aspnet_core.pdf', '/images/aspnet_core.jpg'),
('978-1-491-94528-3', 'Big Data Explained', 'Nancy Hughes', 'Technology', 2023, '2024-12-01', '/ebooks/big_data.pdf', '/images/big_data.jpg'),
('978-0-321-99278-9', 'Design Patterns', 'Martin Fowler', 'Programming', 2018, '2023-11-10', '/ebooks/design_patterns.pdf', '/images/design_patterns.jpg'),
('978-0-7645-7646-7', 'Ethical Hacking', 'Peter Scott', 'Technology', 2022, '2024-01-14', '/ebooks/ethical_hacking.pdf', '/images/ethical_hacking.jpg'),
('978-1-56881-465-5', 'Web Development Essentials', 'Anna Bell', 'Programming', 2020, '2024-02-18', '/ebooks/web_dev.pdf', '/images/web_dev.jpg'),
('978-1-60497-927-2', 'Networking Simplified', 'Samuel Morris', 'Technology', 2021, '2024-03-22', '/ebooks/networking.pdf', '/images/networking.jpg'),
('978-1-4702-2451-6', 'CSS for Beginners', 'George Evans', 'Programming', 2019, '2024-04-26', '/ebooks/css_basics.pdf', '/images/css_basics.jpg'),
('978-0-12-373602-4', 'Linux Commands', 'Victoria Cox', 'Education', 2018, '2024-05-30', '/ebooks/linux_commands.pdf', '/images/linux_commands.jpg'),
('978-0-262-13472-5', 'Functional Programming', 'Kevin Perry', 'Programming', 2023, '2024-06-15', '/ebooks/functional_programming.pdf', '/images/functional_programming.jpg'),
('978-1-86197-876-9', 'Agile Methodologies', 'Lisa Hall', 'Technology', 2022, '2024-07-20', '/ebooks/agile_methods.pdf', '/images/agile_methods.jpg'),
('978-0-596-10105-5', 'Java Advanced', 'Brian Garcia', 'Programming', 2020, '2024-08-24', '/ebooks/java_advanced.pdf', '/images/java_advanced.jpg'),
('978-1-4842-3008-8', 'Cybersecurity Insights', 'Sophia Ward', 'Technology', 2021, '2024-09-28', '/ebooks/cybersecurity.pdf', '/images/cybersecurity.jpg'),
('978-0-596-10117-7', 'HTML Essentials', 'Jack Turner', 'Programming', 2019, '2024-10-10', '/ebooks/html_essentials.pdf', '/images/html_essentials.jpg'),
('978-0-123-40057-6', 'Cloud Computing Guide', 'Ella Phillips', 'Technology', 2023, '2024-11-03', '/ebooks/cloud_computing.pdf', '/images/cloud_computing.jpg'),
('978-1-4028-9467-7', 'NoSQL Databases', 'Ryan Wright', 'Education', 2022, '2024-12-15', '/ebooks/nosql.pdf', '/images/nosql.jpg'),
('978-1-4842-5579-1', 'UX Design Principles', 'Zoe Collins', 'Design', 2021, '2024-01-28', '/ebooks/ux_design.pdf', '/images/ux_design.jpg'),
('978-0-387-98591-7', 'Quantum Computing', 'Daniel Rogers', 'Technology', 2023, '2024-02-11', '/ebooks/quantum_computing.pdf', '/images/quantum_computing.jpg'),
('978-0-123-45678-9', 'Artificial Intelligence', 'Karen Brooks', 'Technology', 2020, '2024-03-19', '/ebooks/artificial_intelligence.pdf', '/images/artificial_intelligence.jpg'),
('978-1-63159-999-0', 'Android App Development', 'Chloe Edwards', 'Programming', 2018, '2024-04-07', '/ebooks/android_dev.pdf', '/images/android_dev.jpg');


-- Inserting 30 sample records into EbookMetadata
INSERT INTO EbookMetadatas (EbookId, FileFormat, FileSize, PageCount, Language, DownloadCount, ViewCount, Publisher, Description, DigitalRights) VALUES
(1, 'PDF', 2.5, 200, 'English', 500, 150, 'TechBooks Inc.', 'Comprehensive guide to coding', 'DRM-Free'),
(2, 'PDF', 1.8, 180, 'English', 400, 120, 'EduPub', 'Simplified data structures for learners', 'DRM-Protected'),
(3, 'EPUB', 3.2, 250, 'English', 450, 200, 'CodeMasters', 'Learn JavaScript basics effectively', 'DRM-Free'),
(4, 'PDF', 4.0, 300, 'English', 600, 300, 'AI Publishers', 'Guide to machine learning fundamentals', 'DRM-Free'),
(5, 'PDF', 2.1, 220, 'English', 550, 180, 'PythonPress', 'Deep dive into Python programming', 'DRM-Protected'),
(6, 'EPUB', 1.5, 150, 'English', 300, 100, 'DB Publishers', 'Introduction to database design', 'DRM-Free'),
(7, 'PDF', 5.0, 320, 'English', 700, 250, 'AI for All', 'AI concepts for all levels', 'DRM-Free'),
(8, 'PDF', 3.5, 270, 'English', 650, 170, 'AngularExperts', 'Master Angular with this guide', 'DRM-Protected'),
(9, 'PDF', 4.2, 290, 'English', 500, 140, 'ReactWorld', 'Learn advanced React techniques', 'DRM-Free'),
(10, 'EPUB', 2.8, 200, 'English', 480, 130, 'CSharpWiz', 'Comprehensive guide to C#', 'DRM-Free'),
(11, 'PDF', 3.0, 230, 'English', 520, 150, 'BlockchainHub', 'Basics of blockchain technology', 'DRM-Protected'),
(12, 'PDF', 2.6, 210, 'English', 470, 140, 'ASP.NET Books', 'Learn ASP.NET Core essentials', 'DRM-Free'),
(13, 'EPUB', 1.7, 160, 'English', 340, 100, 'BigDataPress', 'Explained: Big data concepts', 'DRM-Protected'),
(14, 'PDF', 4.1, 290, 'English', 600, 240, 'DesignPress', 'Introduction to design patterns', 'DRM-Free'),
(15, 'PDF', 1.9, 190, 'English', 450, 170, 'EthicalPress', 'Learn ethical hacking techniques', 'DRM-Free'),
(16, 'EPUB', 2.3, 180, 'English', 360, 110, 'WebDevPub', 'Essentials of web development', 'DRM-Free'),
(17, 'PDF', 3.6, 240, 'English', 550, 180, 'NetMaster', 'Simplified networking concepts', 'DRM-Protected'),
(18, 'PDF', 1.8, 170, 'English', 310, 90, 'CSS Press', 'Learn CSS basics step-by-step', 'DRM-Free'),
(19, 'EPUB', 2.5, 210, 'English', 490, 150, 'LinuxWorld', 'Command reference for Linux', 'DRM-Free'),
(20, 'PDF', 5.5, 320, 'English', 720, 300, 'FunctionalBooks', 'Introduction to functional programming', 'DRM-Free'),
(21, 'PDF', 3.0, 260, 'English', 580, 200, 'AgilePress', 'Agile methodologies explained', 'DRM-Protected'),
(22, 'EPUB', 4.4, 310, 'English', 610, 230, 'JavaExperts', 'Advanced Java techniques', 'DRM-Free'),
(23, 'PDF', 4.0, 280, 'English', 560, 170, 'CyberPress', 'Insights into cybersecurity', 'DRM-Protected'),
(24, 'PDF', 3.2, 210, 'English', 490, 140, 'HTMLBooks', 'Learn HTML essentials', 'DRM-Free'),
(25, 'EPUB', 2.7, 200, 'English', 450, 110, 'CloudPub', 'Introduction to cloud computing', 'DRM-Free'),
(26, 'PDF', 3.8, 270, 'English', 530, 190, 'NoSQL Press', 'NoSQL databases made simple', 'DRM-Protected'),
(27, 'EPUB', 2.0, 180, 'English', 380, 130, 'UX Design Co.', 'Principles of UX design', 'DRM-Free'),
(28, 'PDF', 4.5, 310, 'English', 640, 250, 'QuantumBooks', 'Quantum computing fundamentals', 'DRM-Free'),
(29, 'EPUB', 4.2, 290, 'English', 620, 240, 'AI Press', 'Comprehensive guide to AI', 'DRM-Protected'),
(30, 'PDF', 3.9, 270, 'English', 570, 200, 'Android Experts', 'Develop Android apps with ease', 'DRM-Free');