using System;

namespace AssassinsManager.EntityFramework;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HashedPassword { get; set; }
}