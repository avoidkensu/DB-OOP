using System;

class MainProgram
{
    public static void Main()
    {
        GroupActivity.ManageProduct.InsertNewProduct newProduct = new GroupActivity.ManageProduct.InsertNewProduct();

        newProduct.InsertData(); // No need to pass parameters, user inputs inside the method
    }
}
