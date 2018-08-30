CREATE PROCEDURE PercentOfApprovedCasesPerUser
AS
BEGIN

	SELECT
	 t1.UserName, ISNULL(t2.ApprovedCases, 0) AS ApprovedCases, ISNULL(t2.PercentOfApproved, 0) AS PercentOfApproved
	 FROM
		(SELECT DISTINCT
			anu.UserName as UserName
		FROM dbo.[Case] AS c
		JOIN dbo.AspNetUsers AS anu ON c.WorkerID = anu.Id) t1
	LEFT JOIN
		(SELECT 
			anu.UserName, 
			COUNT(*) AS ApprovedCases,
			(CONVERT(DECIMAL,COUNT(*))/CONVERT(DECIMAL,(SELECT COUNT(*) FROM dbo.[Case]))) * 100 AS PercentOfApproved
		FROM dbo.[Case] AS c
		JOIN dbo.AspNetUsers AS anu ON c.WorkerID = anu.Id
		WHERE c.Status = 3
		GROUP BY anu.UserName) t2
	ON t1.UserName = t2.UserName

END
GO