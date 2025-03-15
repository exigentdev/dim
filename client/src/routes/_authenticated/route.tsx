import { SimpleNavBar } from '@/components/ui/simple-nav-bar';
import { useAuth } from '@/hooks/auth';
import {
  createFileRoute,
  Outlet,
  redirect,
  useNavigate,
} from '@tanstack/react-router';

export const Route = createFileRoute('/_authenticated')({
  component: Authenticated,
  beforeLoad: async ({ context }) => {
    const { isLoggedIn } = context.authentication;
    if (!isLoggedIn()) {
      throw redirect({ to: '/login' });
    }
  },
});

function Authenticated() {
  const auth = useAuth();
  const navigate = useNavigate();

  const logout = () => {
    auth.logout();
    navigate({ to: '/login' });
  };

  return (
    <div>
      <SimpleNavBar isLoggedIn={auth.isLoggedIn()} onLogout={logout} />
      <Outlet />
    </div>
  );
}
