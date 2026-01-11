using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Blism.Wpf;

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

	private const string YamlSource = @"# This is a comment
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

	private const string DartSource = @"import 'dart:async';

void main() async {
  // This is a comment
  print('Hello, World!');
}";

	private const string BashSource = @"#!/bin/bash

# This is a comment
echo 'Hello, World!'

if [[ $1 == '--help' ]]; then
  echo 'Usage: script.sh [OPTIONS]'
  echo 'Options:'
  echo '  -h, --help  Show this help message and exit'
  exit 0
fi
";

	private const string PhpSource = @"<?php
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
