INSERT INTO FoodItems (Name, Description, Price, Category, ImageUrl) VALUES
('Burger', 'Delicious beef burger', 5.99, 'Fast Food', 'https://cookingwithdog.com/wp-content/uploads/2021/06/beef-hamburger-00.jpg'),
('Pizza', 'Cheesy pepperoni pizza', 8.99, 'Fast Food', 'https://alltop.vn/backend/media/images/posts/695/Pizza_4Ps-195791.jpg'),
('Sushi', 'Fresh sushi rolls', 12.99, 'Japanese', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRm9TS8jeYVjeMTnMxW58pTBVRC2GXDvfN42g&s'),
('Salad', 'Healthy green salad', 6.99, 'Healthy', 'https://cdn.loveandlemons.com/wp-content/uploads/2021/04/green-salad.jpg'),
('Taco', 'Spicy chicken taco', 3.99, 'Mexican', 'https://food.fnr.sndimg.com/content/dam/images/food/fullset/2012/2/29/0/0149359_Making-Taco_s4x3.jpg.rend.hgtvcom.1280.960.suffix/1371603491866.jpeg'),
('Pasta', 'Italian pasta with tomato sauce', 7.99, 'Italian', 'https://img.dominos.vn/tim-hieu-cac-loai-pasta-0.jpg'),
('Ice Cream', 'Vanilla ice cream', 2.99, 'Dessert', 'https://www.indianhealthyrecipes.com/wp-content/uploads/2022/04/homemade-ice-cream-recipe.jpg'),
('Sandwich', 'Ham and cheese sandwich', 4.99, 'Fast Food', 'https://static.toiimg.com/thumb/83740315.cms?width=1200&height=900'),
('Soup', 'Hot chicken soup', 5.49, 'Healthy', 'https://downshiftology.com/wp-content/uploads/2023/09/Vegetable-Soup-main-500x375.jpg'),
('Steak', 'Grilled beef steak', 14.99, 'Main Course', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMCBLMiNLCaunzTYj0M8ZIM39cSR8nSquSig&s'),
('Coca-Cola', 'Refreshing Coca-Cola drink', 1.99, 'Beverage', 'https://example.com/cocacola.jpg');

INSERT INTO FoodItems (Name, Description, Price, Category, ImageUrl)
VALUES ('Coca-Cola', 'Refreshing Coca-Cola drink', 1.99, 'Beverage', 'https://product.hstatic.net/1000141988/product/nuoc_ngot_cocacola_vi_nguyen_ban_320_ml_5545f89b5d434c548a8bff6118a3ed49.jpg');

INSERT INTO FoodItems (Name, Description, Price, Category, ImageUrl)
VALUES ('Pepsi', 'Refreshing Pepsi drink', 1.99, 'Beverage', 'https://product.hstatic.net/1000166699/product/16568750047262_1__a06cae5281e14da68881f7c87b67116e_master.jpg');

INSERT INTO FoodItems (Name, Description, Price, Category, ImageUrl)
VALUES ('Sprite', 'Refreshing Sprite drink', 1.99, 'Beverage', 'https://product.hstatic.net/1000141988/product/nuoc_ngot_sprite_330_ml_36218b2593804919860644198e59fa1c.jpg');

INSERT INTO FoodItems (Name, Description, Price, Category, ImageUrl)
VALUES ('Iced Tea', 'Refreshing Iced Tea', 1.99, 'Beverage', 'https://natashaskitchen.com/wp-content/uploads/2021/07/Iced-Tea-3-1-728x1092.jpg');

INSERT INTO FoodItems (Name, Description, Price, Category, ImageUrl)
VALUES ('Orange Juice', 'Fresh Orange Juice', 2.49, 'Beverage', 'https://api.pepperonis.com.vn//uploads/images/1592382820585Orange.jpg');


-- Thêm Combo 1
INSERT INTO Combos (Name, Description, Price)
VALUES ('Combo 1', 'Combo description 1', 19.99);

-- Liên kết Combo 1 với FoodItem
-- Example of corrected INSERT INTO ComboFoodItems statements

-- Combo 1
INSERT INTO ComboFoodItems (ComboId, FoodItemId)
SELECT
    (SELECT Id FROM Combos WHERE Name = 'Combo 1'),
    (SELECT Id FROM FoodItems WHERE Name = 'Burger');

INSERT INTO ComboFoodItems (ComboId, FoodItemId)
SELECT
    (SELECT Id FROM Combos WHERE Name = 'Combo 1'),
    (SELECT Id FROM FoodItems WHERE Name = 'Pizza');

-- Combo 2
INSERT INTO ComboFoodItems (ComboId, FoodItemId)
SELECT
    (SELECT Id FROM Combos WHERE Name = 'Combo 2'),
    (SELECT Id FROM FoodItems WHERE Name = 'Steak');

-- and so on for other Combos and FoodItems

delete from Combos

-- Thêm Combo 1
INSERT INTO Combos (Name, Description, Price)
VALUES ('Combo 1', 'Combo description 1', 19.99);

-- Liên kết Combo 1 với FoodItems
INSERT INTO ComboFoodItems (ComboId, FoodItemId)
SELECT
    c.Id AS ComboId,
    f.Id AS FoodItemId
FROM
    Combos c
    CROSS JOIN FoodItems f
WHERE
    c.Name = 'Combo 1'
    AND f.Name IN ('Burger', 'Pizza', 'Coca-Cola', 'Ice Cream');


-- Thêm Combo 2
INSERT INTO Combos (Name, Description, Price)
VALUES ('Combo 2', 'Combo description 2', 17.99);

-- Liên kết Combo 2 với FoodItems
INSERT INTO ComboFoodItems (ComboId, FoodItemId)
SELECT
    c.Id AS ComboId,
    f.Id AS FoodItemId
FROM
    Combos c
    CROSS JOIN FoodItems f
WHERE
    c.Name = 'Combo 2'
    AND f.Name IN ('Steak', 'Salad', 'Orange Juice');

-- Thêm Combo 3
INSERT INTO Combos (Name, Description, Price)
VALUES ('Combo 3', 'Combo description 3', 15.99);

-- Liên kết Combo 3 với FoodItems
INSERT INTO ComboFoodItems (ComboId, FoodItemId)
SELECT
    c.Id AS ComboId,
    f.Id AS FoodItemId
FROM
    Combos c
    CROSS JOIN FoodItems f
WHERE
    c.Name = 'Combo 3'
    AND f.Name IN ('Taco', 'Pasta', 'Sprite');


-- Thêm Combo 4
INSERT INTO Combos (Name, Description, Price)
VALUES ('Combo 4', 'Combo description 4', 18.99);

-- Liên kết Combo 4 với FoodItems
INSERT INTO ComboFoodItems (ComboId, FoodItemId)
SELECT
    c.Id AS ComboId,
    f.Id AS FoodItemId
FROM
    Combos c
    CROSS JOIN FoodItems f
WHERE
    c.Name = 'Combo 4'
    AND f.Name IN ('Soup', 'Sandwich', 'Iced Tea');

-- Thêm Combo 5
INSERT INTO Combos (Name, Description, Price)
VALUES ('Combo 5', 'Combo description 5', 16.99);

-- Liên kết Combo 5 với FoodItems
INSERT INTO ComboFoodItems (ComboId, FoodItemId)
SELECT
    c.Id AS ComboId,
    f.Id AS FoodItemId
FROM
    Combos c
    CROSS JOIN FoodItems f
WHERE
    c.Name = 'Combo 5'
    AND f.Name IN ('Sushi', 'Coca-Cola', 'Orange Juice');

