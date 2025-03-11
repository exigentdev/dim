import { createFileRoute } from '@tanstack/react-router';
import { useAuth } from '../hooks/auth';

export const Route = createFileRoute('/login')({
  component: Login,
});

function Login() {
  const auth = useAuth();
  return (
    <>
      <div>
        <button
          onClick={() => {
            auth.signIn();
          }}
        >
          login
        </button>
      </div>
      <div>
        <button
          onClick={() => {
            auth.signOut();
          }}
        >
          signout
        </button>
      </div>
    </>
  );
}
