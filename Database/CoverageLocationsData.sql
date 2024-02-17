DELETE FROM dbo.CoverageLocations

SET IDENTITY_INSERT dbo.CoverageLocations ON 

  
INSERT INTO dbo.CoverageLocations([Id], [state], [coverageName],[lga],[longitude], [latitude])
VALUES (1, 'lagos', 'Parkview Coverage Gap', 'ikoyi', 2402483096, 526067116)

 INSERT INTO dbo.CoverageLocations([Id], [state], [coverageName],[lga],[longitude], [latitude])
VALUES (2, 'lagos', 'victory garden city', 'ajah', 2402483095, 526067115)


SET IDENTITY_INSERT dbo.CoverageLocations OFF