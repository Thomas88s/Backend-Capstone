USE [StonkMarket]
GO



SET IDENTITY_INSERT [Stonk] ON
INSERT INTO [Stonk] ([Id], [Name], [Price], [Date], [OneYear], [FiveYear], [TenYear]) 
VALUES (1, 'AMC Entertainment (NYSE:AMC)', 40.93, 10/21/2021, 44.93, 60.93, 88.93), (2, 'GameStop (NYSE:GME)', 182.66, 10/21/2021, 80.66, 40.66, 10.00), 
       (3, 'Koss (NASDAQ:KOSS)', 17.22, 10/21/2021, 19.22, 25.22, 35.10), (4, 'Express (NYSE:EXPR)', 4.28, 10/21/2021, 5.50, 6.60, 9.13), 
       (5, 'Jaguar Health (NASDAQ:JAGX)', 1.97, 10/21/2021, 76.54, 3.65, 0.2236), 
	   (6, 'Sundial Growers (NASDAQ:SNDL)', 0.73, 10/21/2021, 2.84, 33.25, 6.35), (7, 'Zomedica (NYSEAMERICAN:ZOM)', 0.51, 10/21/2021, 1.14, 17.63, 0.25);
SET IDENTITY_INSERT [Stonk] OFF
