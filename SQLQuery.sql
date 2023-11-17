-- After creating your database using the MandelsBankenConsole code, employ this SQL script to insert sample data, simplifying the testing process.

INSERT INTO Users (CustomerName, SocialSecurityNumber, Pin)
VALUES 
  ('Bamse', '19520120-5123', '1234'),
  ('Brummelisa', '19520202-7223', '1122'),
  ('Lille Skutt', '19630620-8123', '3344'),
  ('Skalman', '19460705-4123', '4455'),
  ('Katten Jansson', '19600331-3323', '9999'),
  ('Farmor Skogsmus', '19650429-3223', '5555'),
  ('Donald Duck', '19320610-1106', '8866'),
  ('Daisy Duck', '19340622-3206', '6688'),
  ('Scrooge McDuck', '19300531-4488', '3820'),
  ('Goofy', '19280315-8756', '0000'),
  ('Muminmamma', '19101026-8265', '0102'),
  ('Muminpappa', '19111202-4745', '1842'),
  ('Mumin', '19350830-5528', '3218'),
  ('Snusmumriken', '19121124-2937', '3145'),
  ('Lilla My', '19360512-4456', '4568');

INSERT INTO Currencies (CurrencyCode, CurrencyName)
VALUES
    ('USD', 'United States Dollar'),
    ('AED', 'United Arab Emirates Dirham'),
    ('AFN', 'Afghan Afghani'),
    ('ALL', 'Albanian Lek'),
    ('AMD', 'Armenian Dram'),
    ('ANG', 'Netherlands Antillean Guilder'),
    ('AOA', 'Angolan Kwanza'),
    ('ARS', 'Argentine Peso'),
    ('AUD', 'Australian Dollar'),
    ('AWG', 'Aruban Florin'),
    ('AZN', 'Azerbaijani Manat'),
    ('BAM', 'Bosnia and Herzegovina Convertible Mark'),
    ('BBD', 'Barbadian Dollar'),
    ('BDT', 'Bangladeshi Taka'),
    ('BGN', 'Bulgarian Lev'),
    ('BHD', 'Bahraini Dinar'),
    ('BIF', 'Burundian Franc'),
    ('BMD', 'Bermudian Dollar'),
    ('BND', 'Brunei Dollar'),
    ('BOB', 'Bolivian Boliviano'),
    ('BRL', 'Brazilian Real'),
    ('BSD', 'Bahamian Dollar'),
    ('BTN', 'Bhutanese Ngultrum'),
    ('BWP', 'Botswana Pula'),
    ('BYN', 'Belarusian Ruble'),
    ('BZD', 'Belize Dollar'),
    ('CAD', 'Canadian Dollar'),
    ('CDF', 'Congolese Franc'),
    ('CHF', 'Swiss Franc'),
    ('CLP', 'Chilean Peso'),
    ('CNY', 'Chinese Yuan'),
    ('COP', 'Colombian Peso'),
    ('CRC', 'Costa Rican Colón'),
    ('CUP', 'Cuban Peso'),
    ('CVE', 'Cape Verdean Escudo'),
    ('CZK', 'Czech Republic Koruna'),
    ('DJF', 'Djiboutian Franc'),
    ('DKK', 'Danish Krone'),
    ('DOP', 'Dominican Peso'),
    ('DZD', 'Algerian Dinar'),
    ('EGP', 'Egyptian Pound'),
    ('ERN', 'Eritrean Nakfa'),
    ('ETB', 'Ethiopian Birr'),
    ('EUR', 'Euro'),
    ('FJD', 'Fijian Dollar'),
    ('FKP', 'Falkland Islands Pound'),
    ('FOK', 'Faroese Króna'),
    ('GBP', 'British Pound Sterling'),
    ('GEL', 'Georgian Lari'),
    ('GGP', 'Guernsey Pound'),
    ('GHS', 'Ghanaian Cedi'),
    ('GIP', 'Gibraltar Pound'),
    ('GMD', 'Gambian Dalasi'),
    ('GNF', 'Guinean Franc'),
    ('GTQ', 'Guatemalan Quetzal'),
    ('GYD', 'Guyanese Dollar'),
    ('HKD', 'Hong Kong Dollar'),
    ('HNL', 'Honduran Lempira'),
    ('HRK', 'Croatian Kuna'),
    ('HTG', 'Haitian Gourde'),
    ('HUF', 'Hungarian Forint'),
    ('IDR', 'Indonesian Rupiah'),
    ('ILS', 'Israeli New Shekel'),
    ('IMP', 'Isle of Man Pound'),
    ('INR', 'Indian Rupee'),
    ('IQD', 'Iraqi Dinar'),
    ('IRR', 'Iranian Rial'),
    ('ISK', 'Icelandic Króna'),
    ('JEP', 'Jersey Pound'),
    ('JMD', 'Jamaican Dollar'),
    ('JOD', 'Jordanian Dinar'),
    ('JPY', 'Japanese Yen'),
    ('KES', 'Kenyan Shilling'),
    ('KGS', 'Kyrgyzstani Som'),
    ('KHR', 'Cambodian Riel'),
    ('KID', 'Kiribati Dollar'),
    ('KMF', 'Comorian Franc'),
    ('KRW', 'South Korean Won'),
    ('KWD', 'Kuwaiti Dinar'),
    ('KYD', 'Cayman Islands Dollar'),
    ('KZT', 'Kazakhstani Tenge'),
    ('LAK', 'Laotian Kip'),
    ('LBP', 'Lebanese Pound'),
    ('LKR', 'Sri Lankan Rupee'),
    ('LRD', 'Liberian Dollar'),
    ('LSL', 'Lesotho Loti'),
    ('LYD', 'Libyan Dinar'),
    ('MAD', 'Moroccan Dirham'),
    ('MDL', 'Moldovan Leu'),
    ('MGA', 'Malagasy Ariary'),
    ('MKD', 'Macedonian Denar'),
    ('MMK', 'Myanmar Kyat'),
    ('MNT', 'Mongolian Tugrik'),
    ('MOP', 'Macanese Pataca'),
    ('MRU', 'Mauritanian Ouguiya'),
    ('MUR', 'Mauritian Rupee'),
    ('MVR', 'Maldivian Rufiyaa'),
    ('MWK', 'Malawian Kwacha'),
    ('MXN', 'Mexican Peso'),
    ('MYR', 'Malaysian Ringgit'),
    ('MZN', 'Mozambican Metical'),
    ('NAD', 'Namibian Dollar'),
    ('NGN', 'Nigerian Naira'),
    ('NIO', 'Nicaraguan Córdoba'),
    ('NOK', 'Norwegian Krone'),
    ('NPR', 'Nepalese Rupee'),
    ('NZD', 'New Zealand Dollar'),
    ('OMR', 'Omani Rial'),
    ('PAB', 'Panamanian Balboa'),
    ('PEN', 'Peruvian Nuevo Sol'),
    ('PGK', 'Papua New Guinean Kina'),
    ('PHP', 'Philippine Peso'),
    ('PKR', 'Pakistani Rupee'),
    ('PLN', 'Polish Złoty'),
    ('PYG', 'Paraguayan Guarani'),
    ('QAR', 'Qatari Rial'),
    ('RON', 'Romanian Leu'),
    ('RSD', 'Serbian Dinar'),
    ('RUB', 'Russian Ruble'),
    ('RWF', 'Rwandan Franc'),
    ('SAR', 'Saudi Riyal'),
    ('SBD', 'Solomon Islands Dollar'),
    ('SCR', 'Seychellois Rupee'),
    ('SDG', 'Sudanese Pound'),
    ('SEK', 'Swedish Krona'),
    ('SGD', 'Singapore Dollar'),
    ('SHP', 'Saint Helena Pound'),
    ('SLE', 'Sierra Leonean Leone');

INSERT INTO Accounts (AccountNumber, AccountName, Type, Balance, UserId, CurrencyId)
VALUES 
  (1001, 'Lönekonto', 1, 3000.00, 1, 125), -- Bamse UserId, SEK CurrencyId
  (1002, 'Månadskonto', 1, 3000.00, 2, 125), -- Brummelisa UserId, SEK CurrencyId
  (1003, 'Lönekonto', 1, 2000.00, 3, 125), -- Lille Skutt UserId, SEK CurrencyId
  (1004, 'Lönekonto', 1, 8000.00, 4, 125), -- Skalman UserId, SEK CurrencyId
  (1005, 'Lönekonto', 1, 2500.00, 5, 125), -- Katten Jansson UserId, SEK CurrencyId
  (1006, 'Månadskonto', 1, 1500.00, 6, 125), -- Farmor Skogsmus UserId, SEK CurrencyId
  (1007, 'Checking', 1, 500.00, 7, 1), -- Donald Duck UserId, USD CurrencyId
  (1008, 'Checking', 1, 500.00, 8, 1), -- Daisy Duck UserId, USD CurrencyId
  (1009, 'Checking', 1, 10.00, 9, 1), -- Scrooge McDuck UserId, USD CurrencyId
  (1010, 'Checking', 1, 5000.00, 10, 1), -- Goofy UserId, USD CurrencyId
  (1011, 'Månadskonto', 1, 300, 11, 44), -- Muminmamma UserId, EUR CurrencyId
  (1012, 'Lönekonto', 1, 300, 12, 44), -- Muminpappa UserId, EUR CurrencyId
  (1013, 'Månadskonto', 1, 10, 13, 44), -- Mumin UserId, EUR CurrencyId
  (1014, 'Månadskonto', 1, 50, 14, 44), -- Snusmumriken UserId, EUR CurrencyId
  (1015, 'Månadskonto', 1, 5, 15, 44), -- Lilla My UserId, EUR CurrencyId
  (2001, 'Sparkonto', 2, 8000.00, 1, 125), -- Bamse UserId, SEK CurrencyId
  (2002, 'Husrenoveringsbesparing', 2, 20000.00, 1, 125), -- Bamse UserId, SEK CurrencyId
  (2003, 'Sparkonto', 2, 8000.00, 2, 125), -- Brummelisa UserId, SEK CurrencyId
  (2004, 'Resesparkonto', 2, 10000.00, 3, 125), -- Lille Skutt UserId, SEK CurrencyId
  (2005, 'Innovationssparande', 2, 800000.00, 4, 125), -- Skalman UserId, SEK CurrencyId
  (2006, 'Dunderspar', 2, 1000000.00, 4, 125), -- Skalman UserId, SEK CurrencyId
  (2007, 'Sparkonto', 2, 2500.00, 5, 125), -- Katten Jansson UserId, SEK CurrencyId
  (2008, 'Sparkonto', 2, 10000.00, 6, 125), -- Farmor Skogsmus UserId, SEK CurrencyId
  (2009, 'Savings', 2, 50.00, 7, 1), -- Donald Duck UserId, USD CurrencyId
  (2010, 'Savings', 2, 50.00, 8, 1), -- Daisy Duck UserId, USD CurrencyId
  (2011, 'Divorce fund', 2, 20000.00, 8, 1), -- Daisy Duck UserId, USD CurrencyId
  (2012, 'Cash vault', 2, 10000000000.00, 9, 1), -- Scrooge McDuck UserId, USD CurrencyId
  (2013, 'Savings', 2, 100.00, 10, 1), -- Goofy UserId, USD CurrencyId
  (2014, 'Other savings', 2, 100.00, 10, 1), -- Goofy UserId, USD CurrencyId
  (2015, 'Other other savings', 2, 100.00, 10, 1), -- Goofy UserId, USD CurrencyId
  (2016, 'Sparkonto', 2, 300.00, 11, 44), -- Muminmamma UserId, EUR CurrencyId
  (2017, 'Sparkonto', 2, 300.00, 12, 44), -- Muminpappa UserId, EUR CurrencyId
  (2018, 'Arvssparande', 2, 10000.00, 12, 44), -- Muminpappa UserId, EUR CurrencyId
  (2019, 'Äventyrssparande', 2, 100.00, 13, 44), -- Mumin UserId, EUR CurrencyId
  (2020, 'Resesparkonto', 2, 1000.00, 14, 44), -- Snusmumriken UserId, EUR CurrencyId
  (2021, 'Jättebesparing', 2, 50.00, 15, 44); -- Lilla My UserId, EUR CurrencyId