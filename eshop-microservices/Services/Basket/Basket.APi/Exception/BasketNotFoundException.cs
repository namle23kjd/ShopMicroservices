﻿

namespace Basket.APi.Exception;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string userName) : base("Basket", userName)
    {
        
    }
}