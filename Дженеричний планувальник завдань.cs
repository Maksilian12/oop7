using System;
using System.Collections.Generic;

public class TaskScheduler<TTask, TPriority>
{
    private readonly List<TaskWithPriority> taskQueue = new List<TaskWithPriority>();
    private Func<TTask, TPriority> getTaskPriority;

    public TaskScheduler(Func<TTask, TPriority> getTaskPriority)
    {
        this.getTaskPriority = getTaskPriority;
    }

    public void AddTask(TTask task)
    {
        var priority = getTaskPriority(task);
        taskQueue.Add(new TaskWithPriority(task, priority));
        taskQueue.Sort((t1, t2) => Comparer<TPriority>.Default.Compare(t1.Priority, t2.Priority));
    }

    public void ExecuteNext(Action<TTask> taskExecutor)
    {
        if (taskQueue.Count > 0)
        {
            var nextTask = taskQueue[0].Task;
            taskExecutor(nextTask);
            taskQueue.RemoveAt(0);
        }
        else
        {
            Console.WriteLine("No tasks in the queue.");
        }
    }

    public int GetTaskCount()
    {
        return taskQueue.Count;
    }

    public void ReturnTaskToPool(TTask task)
    {
        AddTask(task);
    }

    public void ClearTaskQueue()
    {
        taskQueue.Clear();
    }

    private class TaskWithPriority
    {
        public TTask Task { get; }
        public TPriority Priority { get; }

        public TaskWithPriority(TTask task, TPriority priority)
        {
            Task = task;
            Priority = priority;
        }
    }
}

class Program
{
    static void Main()
    {
        // Приклад використання TaskScheduler з пріоритетами int
        var scheduler = new TaskScheduler<string, int>(task => task.Length);

        scheduler.AddTask("Task 1");
        scheduler.AddTask("Task 2");
        scheduler.AddTask("Task 3");

        Console.WriteLine("Tasks in the queue: " + scheduler.GetTaskCount());

        scheduler.ExecuteNext(task => Console.WriteLine("Executing task: " + task));
        scheduler.ExecuteNext(task => Console.WriteLine("Executing task: " + task));

        Console.WriteLine("Tasks in the queue: " + scheduler.GetTaskCount());

        scheduler.ClearTaskQueue();

        Console.WriteLine("Tasks in the queue: " + scheduler.GetTaskCount());
    }
}
