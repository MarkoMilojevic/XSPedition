DELETE FROM CaTypeFieldMap
DELETE FROM OptionTypeFieldMap
DELETE FROM PayoutTypeFieldMap

DELETE FROM CaTypeRegistry
DELETE FROM OptionTypeRegistry
DELETE FROM PayoutTypeRegistry
DELETE FROM FieldRegistry

DELETE FROM ScrubbingInfo

SET IDENTITY_INSERT CaTypeRegistry ON
--INSERT CATYPERegistry
INSERT INTO [dbo].[CaTypeRegistry]
           ([CaTypeRegistryId], [Code])
     VALUES
           (1, 'DRIP'), (2, 'DVOP'), (3, 'PINK'), (4, 'CONV'),
		   (5, 'PCAL'), (6, 'RHTS'), (7, 'TEND'), (8, 'INTR'),
		   (9, 'DVCA')
SET IDENTITY_INSERT CaTypeRegistry OFF


SET IDENTITY_INSERT OptionTypeRegistry ON
--INSERT OPTIONTYPERegistry
INSERT INTO [dbo].[OptionTypeRegistry]
           ([OptionTypeRegistryId], [Code])
     VALUES
           (1, 'Cash'), (2, 'Security'), (3, 'Cash and Security'), (4, 'Exercise'),
		   (5, 'No Action')
SET IDENTITY_INSERT OptionTypeRegistry OFF


SET IDENTITY_INSERT PayoutTypeRegistry ON
--INSERT PAYOUTTYPERegistry
INSERT INTO [dbo].[PayoutTypeRegistry]
           ([PayoutTypeRegistryId], [Code])
     VALUES
           (1, 'Principal Cash'), (2, 'Security'), (3, 'Interest'), (4, 'Dividend')
SET IDENTITY_INSERT PayoutTypeRegistry OFF


SET IDENTITY_INSERT FieldRegistry ON
--INSERT FIELDRegistry
INSERT INTO [dbo].[FieldRegistry]
           ([FieldRegistryId]
		   ,[FieldDisplay]
           ,[FieldType])
     VALUES
			--CA Polja
           (1,'Announcement Date','DATE'),
		   (2,'Base Denomination','NUMBER'),
		   (3,'CA Cancelled','BOOL'),
		   (4,'Effective Date','DATE'),
		   (5,'Ex Date','DATE'),
		   (6,'Interest Period','STRING'),
		   (7,'Lottery Date','DATE'),
		   (8,'Offeror','STRING'),
		   (9,'Publication Date','DATE'),
		   (10,'Record Date','DATE'),

		   --Option polja
		   (101,'Expiration Date','DATE'),
		   (102,'Market date','DATE'),
		   (103,'Minimum Quantity To Instruct','NUMBER'),
		   (104,'Option Active','BOOL'),
		   (105,'Proration Rate','NUMBER'),
		   (106,'Response Due Date','DATE'),
		   (107,'Subscription Date','DATE'),

		   --Payout
		   (1001,'Currency','STRING'),
		   (1002,'Fractional Share Rule','NUMBER'),
		   (1003,'Gross Amount','NUMBER'),
		   (1004,'Interest Rate','NUMBER'),
		   (1005,'Net Amount','NUMBER'),
		   (1006,'New Shares','NUMBER'),
		   (1007,'Old Shares','NUMBER'),
		   (1008,'Payable Date','DATE'),
		   (1009,'Payment Date','DATE'),
		   (1010,'Payout Security ID','STRING'),
		   (1011,'Payout Security ID Type','STRING'),
		   (1012,'Price','NUMBER'),
		   (1013,'Rate Type','STRING'),
		   (1014,'Value Date','DATE'),
		   (1015,'Withholding Tax Rate','NUMBER')
SET IDENTITY_INSERT FieldRegistry OFF


--Insert Field Mappings
--INSERT INTO [dbo].[CaTypeFieldMap]
--			([CaTypeRegistryId],[FieldRegistryId])
--     VALUES
--			(<CaTypeRegistryId, int,>, <FieldRegistryId, int,>),
--			(<CaTypeRegistryId, int,>, <FieldRegistryId, int,>),
--			(<CaTypeRegistryId, int,>, <FieldRegistryId, int,>)
--
----Option polja po OPTION TIPOVIMA
--INSERT INTO [dbo].[OptionTypeFieldMap]
--			([OptionTypeRegistryId],[FieldRegistryId])
--     VALUES
--			(<OptionTypeRegistryId, int,>,<FieldRegistryId, int,>),
--			(<OptionTypeRegistryId, int,>,<FieldRegistryId, int,>)
--
----Payout polja po PAYOUT TIPOVIMA
--INSERT INTO [dbo].[PayoutTypeFieldMap]
--			([PayoutTypeRegistryId],[FieldRegistryId])
--     VALUES
--			(<PayoutTypeRegistryId, int,>,<FieldRegistryId, int,>),
--			(<PayoutTypeRegistryId, int,>,<FieldRegistryId, int,>)
