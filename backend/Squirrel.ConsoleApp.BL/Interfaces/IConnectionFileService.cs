﻿using Squirrel.ConsoleApp.Models;

namespace Squirrel.ConsoleApp.BL.Interfaces;

public interface IConnectionFileService
{
    void CreateEmptyFile();
    ConnectionString ReadFromFile();
    void SaveToFile(ConnectionString connectionString);
}