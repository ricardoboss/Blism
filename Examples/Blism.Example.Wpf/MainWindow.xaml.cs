using System.Windows;

namespace Blism.Example.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

	public const string CSharpSource = @"using System;

namespace ConsoleApp;

class Program
{
    static void Main(string[] arguments)
    {
        // This is a comment
        Console.WriteLine(""Hello, World!"");
    }
}
";

	public const string YamlSource = @"# This is a comment
name: Blism
version: 1.0.0
quotes: 'This is a string'

# This is a list
list:
  - 1
  - 2
  - 3

# This is a dictionary
dictionary:
  key1: value1
  key2: value2
    nested:
      key3: value3
";

	public const string DartSource = @"import 'dart:async';

void main() async {
  // This is a comment
  print('Hello, World!');
}";

	public const string BashSource = @"#!/bin/bash

# This is a comment
echo 'Hello, World!'

if [[ $1 == '--help' ]]; then
  echo 'Usage: script.sh [OPTIONS]'
  echo 'Options:'
  echo '  -h, --help  Show this help message and exit'
  exit 0
fi
";

	public const string PhpSource = @"<?php
declare(strict_types=1);

namespace App\Commands;

use App\Services\Console as Output;

readonly class HelloCommand {
	use Output {
		Output::println as writeln;
	}

	public function __construct(
		private string $who,
	) {
		// greet the person
		$this->writeln(""Hello, $who!"");
	}
}";

	public MainWindow()
	{
		InitializeComponent();
	}
}
