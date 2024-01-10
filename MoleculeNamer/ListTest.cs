List<List<string>> myList = new List<List<string>>();
// Iterate through tree
{
    List<string> newList = new List<string>();
    newList.Add("a");
    newList.Add("b");
    newList.Add("c");
    myList.Add(newList);
}

myList.Add(new List<string> { "a", "b" });
myList.Add(new List<string> { "c", "d", "e" });
myList.Add(new List<string> { "qwerty", "asdf", "zxcv" });
myList.Add(new List<string> { "a", "b" });

// To iterate over it.
foreach (List<string> subList in myList)
{
    foreach (string item in subList)
    {
        Console.WriteLine(item);
    }
}