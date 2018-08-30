CREATE PROCEDURE PercentOfApprovedCasesPerUser
AS
BEGIN
	SELECT 
		anu.UserName, 
		COUNT(*) AS Cases,
		(CONVERT(DECIMAL,COUNT(*))/CONVERT(DECIMAL,(SELECT COUNT(*) FROM dbo.[Case]))) * 100 AS 'Percent'
	FROM dbo.[Case] AS c
	JOIN dbo.AspNetUsers AS anu ON c.WorkerID = anu.Id
	WHERE c.Status = 3
	GROUP BY anu.UserName
END
GO