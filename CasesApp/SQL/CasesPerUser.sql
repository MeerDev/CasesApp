CREATE PROCEDURE CasesPerUser
	@WorkerId nvarchar(100)
AS
BEGIN
	SELECT 
		anu.UserName, 
		CASE WHEN c.Status =  0 THEN 'Pending' WHEN c.Status = 1 THEN 'PendingReview' WHEN c.Status = 2 THEN 'PendingApproval' WHEN c.Status = 3 THEN 'Approved' END as Status,
		c.DateCreated,
		c.DateReviewed,
		c.DateApproved,
		c.ReviewerEmail,
		c.ApproverEmail,
		c.WorkerID
	FROM dbo.[Case] AS c
	JOIN dbo.AspNetUsers AS anu ON c.WorkerID = anu.Id
	WHERE @WorkerId = c.WorkerID
	

END
GO