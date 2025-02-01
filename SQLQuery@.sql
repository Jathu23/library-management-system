

select * from Admins
select * from Users
select * from LoginPort


select * from Audiobooks
select * from AudiobookMetadatas

select * from NormalBooks
select * from BookCopies
select * from LentRecords
select * from RentHistory

select * from Ebooks
select * from EbookMetadatas

delete from Ebooks
delete from EbookMetadatas
delete from NormalBooks
delete from BookCopies



delete from Audiobooks
delete from AudiobookMetadatas
delete from Admins where id = 1005;



DBCC CHECKIDENT ('Ebooks', RESEED, 0);
DBCC CHECKIDENT ('EbookMetadatas', RESEED, 0);


DBCC CHECKIDENT ('NormalBooks', RESEED, 0);
DBCC CHECKIDENT ('BookCopies', RESEED, 0);

DBCC CHECKIDENT ('Audiobooks', RESEED, 0);
DBCC CHECKIDENT ('AudiobookMetadatas', RESEED, 0);

select * from globalSubscriptions

select * from SubscriptionPlan

select * from Users
select * from UserSubscription
select * from Payment
select * from PaymentDuration



select * from NormalBookLikeDislikes


select * from AudiobookLikeDislikes

select * from EbookLikeDislikes
select * from EbookReviews

select * from Admins

select * from EmailTemplates
delete from EmailTemplates


select * from ForgotPasswordTokens

