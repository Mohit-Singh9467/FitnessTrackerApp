-- Create the Category table
CREATE TABLE Category (
    ID INT PRIMARY KEY AUTO_INCREMENT,  -- Unique identifier for each category
    Name VARCHAR(100) NOT NULL,         -- Name of the category
    Description TEXT                    -- Brief description of the category
);

-- Create the Supplier table
CREATE TABLE Supplier (
    ID INT PRIMARY KEY AUTO_INCREMENT,  -- Unique identifier for each supplier
    Name VARCHAR(100) NOT NULL,         -- Name of the supplier
    ContactInfo VARCHAR(255),           -- Contact details of the supplier
    Address TEXT                        -- Address of the supplier
);

-- Create the FitnessEquipment table (optional if EquipmentType_id is required)
CREATE TABLE FitnessEquipment (
    ID INT PRIMARY KEY AUTO_INCREMENT,  -- Unique identifier for each equipment type
    Name VARCHAR(100) NOT NULL,         -- Name of the fitness equipment type
    Description TEXT                    -- Description of the fitness equipment type
);

-- Create the Item table
CREATE TABLE Item (
    ID INT PRIMARY KEY AUTO_INCREMENT,  -- Unique identifier for each item
    Name VARCHAR(100) NOT NULL,         -- Name of the item
    Description TEXT,                   -- Detailed description of the item
    Quantity INT NOT NULL,              -- Available quantity of the item
    EquipmentType_id INT,               -- Foreign key to FitnessEquipment
    Category_id INT NOT NULL,           -- Foreign key to Category
    Supplier_id INT NOT NULL,           -- Foreign key to Supplier
    -- Establish relationships
    FOREIGN KEY (EquipmentType_id) REFERENCES FitnessEquipment(ID) ON DELETE SET NULL,
    FOREIGN KEY (Category_id) REFERENCES Category(ID) ON DELETE CASCADE,
    FOREIGN KEY (Supplier_id) REFERENCES Supplier(ID) ON DELETE CASCADE
);
