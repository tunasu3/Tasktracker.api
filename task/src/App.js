import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import TaskList from './TaskList';
import AddTask from './AddTask';

function App() {
  return (
    <Router>
      <nav>
        <Link to="/">Tasks</Link> | <Link to="/add">Add Task</Link>
      </nav>
      <hr />
      <Routes>
        <Route path="/" element={<TaskList />} />
        <Route path="/add" element={<AddTask />} />
      </Routes>
    </Router>
  );
}