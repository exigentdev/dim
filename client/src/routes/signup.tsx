import { createFileRoute, useNavigate } from '@tanstack/react-router';
import { SignupForm } from '@/components/signup-form';
import { SimpleNavBar } from '@/components/simple-nav-bar';
import { z } from 'zod';
import { useMutation } from '@tanstack/react-query';
import { registerUser } from '@/api/authentication';
import { useAuth } from '@/hooks/auth';
import { AxiosError } from 'axios';
import { toast } from 'sonner';
import { RegisterDto } from 'types/register-dto';

export const Route = createFileRoute('/signup')({
  component: Signup,
});

const passwordSchema = z
  .string()
  .min(12, 'Password must be at least 12 characters long')
  .regex(/[0-9]/, 'Password must contain at least one digit')
  .regex(/[a-z]/, 'Password must contain at least one lowercase letter')
  .regex(/[A-Z]/, 'Password must contain at least one uppercase letter')
  .regex(
    /[^a-zA-Z0-9]/,
    'Password must contain at least one special character',
  );

const signupSchema = z
  .object({
    username: z
      .string()
      .min(1)
      .regex(/^[a-zA-Z0-9]+$/, 'Username must be alphanumeric'),
    email: z.string().email(),
    password: passwordSchema,
    confirmPassword: z.string().min(1),
  })
  .superRefine(({ password, confirmPassword }, ctx) => {
    if (confirmPassword !== password) {
      ctx.addIssue({
        code: 'custom',
        message: 'The passwords do not match',
        path: ['confirmPassword'],
      });
    }
  });

function Signup() {
  const { login } = useAuth();
  const navigate = useNavigate();

  const signupMutation = useMutation({
    mutationKey: ['registerUser'],
    mutationFn: registerUser,
    onSuccess: ({ token }) => {
      navigate({ to: '/posts' });
      login(token);
    },
    onError: (error: AxiosError) => {
      if (error.response) {
        const messages = error.response.data as {
          code: string;
          description: string;
        }[];

        messages.forEach((message) => {
          toast.error(message.description);
        });
      }
    },
  });

  const onSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const registerInfo = Object.fromEntries(formData.entries());
    const registerInfoResult = signupSchema.safeParse(registerInfo);

    if (registerInfoResult.success) {
      const registerDto: RegisterDto = {
        userName: registerInfoResult.data.username,
        email: registerInfoResult.data.email,
        password: registerInfoResult.data.password,
      };

      signupMutation.mutate(registerDto);
    } else {
      registerInfoResult.error.errors.forEach((error) => {
        toast.error(error.message);
      });
    }
  };

  return (
    <div>
      <SimpleNavBar />
      <div className="flex min-h-svh w-full items-center justify-center p-6 md:p-10">
        <div className="w-full max-w-sm">
          <SignupForm onSignupSubmit={onSubmit} />
        </div>
      </div>
    </div>
  );
}
