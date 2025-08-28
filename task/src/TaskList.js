import React, { useEffect, useState } from 'react';

const TaskList = () => {
  const [tasks, setTasks] = useState([]);
  const [editingId, setEditingId] = useState(null);
  const [editedTitle, setEditedTitle] = useState('');

  const fetchTasks = async () => {
    const response = await fetch('https://localhost:7275/api/Tasks/GetAll');
    const data = await response.json();
    setTasks(data);
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  const deleteTask = async (id) => {
    await fetch(`https://localhost:7275/api/Tasks/${id}`, { method: 'DELETE' });
    fetchTasks();
  };

  const startEditing = (task) => {
    setEditingId(task.id);
    setEditedTitle(task.title);
  };

  const updateTask = async (id) => {
    await fetch(`https://localhost:7275/api/Tasks/${id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ id, title: editedTitle }),
    });
    setEditingId(null);
    fetchTasks();
  };

  return (
    <div>
      <h2>Task List</h2>
      <ul>
        {tasks.map((task) => (
          <li key={task.id}>
            {editingId === task.id ? (
              <>
                <input
                  value={editedTitle}
                  onChange={(e) => setEditedTitle(e.target.value)}
                />
                <button onClick={() => updateTask(task.id)}>Save</button>
                <button onClick={() => setEditingId(null)}>Cancel</button>
              </>
            ) : (
              <>
                {task.title}
                <button onClick={() => startEditing(task)} style={{ marginLeft: '10px' }}>
                  Edit
                </button>
                <button onClick={() => deleteTask(task.id)} style={{ marginLeft: '5px' }}>
                  Delete
                </button>
              </>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TaskList;