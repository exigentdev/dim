import { createFileRoute, useNavigate } from '@tanstack/react-router';
import { useAuth } from '../hooks/auth';
import { LoginForm } from '@/components/login-form';
import { useMutation } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import { toast } from 'sonner';
import { z } from 'zod';
import { LoginDto } from '../../types/login-dto';
import { loginUser } from '@/api/authentication';
import { SimpleNavBar } from '@/components/simple-nav-bar';

export const Route = createFileRoute('/login')({
  component: Login,
});

const loginSchema = z.object({
  username: z.string().min(1),
  password: z.string(),
});

function Login() {
  const { login } = useAuth();
  const navigate = useNavigate();

  const loginMutation = useMutation({
    mutationKey: ['loginUser'],
    mutationFn: loginUser,
    onSuccess: ({ token }) => {
      navigate({ to: '/posts' });
      login(token);
    },
    onError: (error: AxiosError) => {
      if (error.response) {
        const message = error.response.data as string;
        toast.error(message);
      }
    },
  });

  const onSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const loginInfo = Object.fromEntries(formData.entries());
    const loginInfoResult = loginSchema.safeParse(loginInfo);

    if (!loginInfoResult.success) {
      loginInfoResult.error.errors.forEach((error) => {
        toast.error(error.message);
      });

      return;
    }

    const loginDto: LoginDto = {
      userName: loginInfoResult.data.username,
      password: loginInfoResult.data.password,
    };

    loginMutation.mutate(loginDto);
  };

  return (
    <div>
      <SimpleNavBar />
      <div className="flex min-h-svh w-full items-center justify-center p-6 md:p-10">
        <div className="w-full max-w-sm">
          <LoginForm onLoginSubmit={onSubmit} />
        </div>
      </div>
    </div>
  );
}
