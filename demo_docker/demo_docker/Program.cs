
//static void Main(string[] args)
//{
List<int> a = new List<int> { 100, 90, 90, 80 };
List<int> b = new List<int> { 4, 6, 5, 3, 3, 1 };

int max = 0;
int curr = 1;
a.Sort();

int item = a[0];
for (int i = 1; i < a.Count; i++)
{
    if (Math.Abs(a[i] - item) <= 1)
    {
        curr++;
    }
    else
    {
        if (max < curr)
        {
            max = curr;
        }
        item = a[i];
        curr = 1;
    }

    Console.WriteLine($"Max = {max}, currentCount = {curr}, item = {item}, i = {i}, current item = {a[i]}, diferencia = {Math.Abs(a[i] - item)}");

}
Console.WriteLine($"Max = {max}, currentCount = {curr}, item = {item}");
if (max < curr)
{
    max = curr;
}

Console.WriteLine(max);
//}
