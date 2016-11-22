CREATE TABLE [dbo].[Recipes] (
    [RecipeId]                INT            IDENTITY (1, 1) NOT NULL  PRIMARY KEY,
    [Name]                    NVARCHAR (MAX) NULL,
    [Description]             NVARCHAR (MAX) NULL,
    [Ingredient_IngredientId] INT            NULL,
    CONSTRAINT [PK_dbo.Recipes] PRIMARY KEY CLUSTERED ([RecipeId] ASC),
    CONSTRAINT [FK_dbo.Recipes_dbo.Ingredients_Ingredient_IngredientId] FOREIGN KEY ([Ingredient_IngredientId]) REFERENCES [dbo].[Ingredients] ([IngredientId])
);