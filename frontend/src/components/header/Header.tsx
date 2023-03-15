import { ReactNode, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getProfile } from '../../api/AuthApi';
import { useAuth } from '../../contexts/AuthContext';
function Header() {
    const [darkmode, setDarkmode] = useState<boolean>(
        (localStorage.getItem('data-theme') ?? 'dark') == 'dark'
    );
    const [state, setState] = useState<number>(0);
    const auth = useAuth();
    const [isAdmin, setIsAdmin] = useState<boolean>(false);
    const toggleDarkMode = (darkmode: boolean) => {
        setDarkmode(darkmode);
        localStorage.setItem('data-theme', darkmode ? 'dark' : 'light');
        document.documentElement.setAttribute('data-theme', darkmode ? 'dark' : 'light');
    };
    useEffect(() => {
        document.documentElement.setAttribute('data-theme', darkmode ? 'dark' : 'light');
        getProfile().then((response) => setIsAdmin(response.data.role === 'admin'));
    }, []);

    const ButtonText = ({ children }: { children: ReactNode }) => {
        return <b className="hover:text-t-hover">{children}</b>;
    };

    return (
        <div>
            {auth.user !== undefined ? (
                <div className="h-[50px] bg-bg-secondary flex">
                    <div className="flex-1"></div>
                    <div className="mt-4 flex flex-col items-end">
                        <button>
                            <span
                                className="material-symbols-outlined cursor-pointer select-none"
                                onClick={() => setState(state == 2 ? 0 : 2)}>
                                settings
                            </span>
                        </button>
                        <button
                            onClick={() => {
                                toggleDarkMode(!(darkmode ?? false));
                                setState(0);
                            }}>
                            {state === 2 && (
                                <ul className="mt-4 bg-bg-secondary p-3">
                                    {darkmode ?? false ? (
                                        <span className="material-symbols-outlined align-middle pr-3">
                                            light_mode
                                        </span>
                                    ) : (
                                        <span className="material-symbols-outlined align-middle pr-3">
                                            dark_mode
                                        </span>
                                    )}
                                    Toggle {darkmode ?? false ? 'light' : 'dark'} mode
                                </ul>
                            )}
                        </button>
                    </div>
                    <div className="w-[50px] ml-[40px] mr-[20px] mt-4">
                        <button>
                            <span
                                className="material-symbols-outlined  cursor-pointer select-none"
                                onClick={() => setState(state == 3 ? 0 : 3)}>
                                manage_accounts
                            </span>
                        </button>
                        {state === 3 && (
                            <ul className="relative w-[120px] right-[70px] mt-4 bg-bg-secondary p-3">
                                <Link onClick={() => setState(0)} to="/profile" className="pb-1">
                                    <span className="material-symbols-outlined pr-3 align-middle">
                                        person
                                    </span>
                                    Profile
                                </Link>
                                <button
                                    className="pt-1"
                                    onClick={() => {
                                        auth.logout();
                                        setState(0);
                                    }}>
                                    <span className="material-symbols-outlined pr-3 align-middle">
                                        logout
                                    </span>
                                    Logout
                                </button>
                            </ul>
                        )}
                    </div>
                </div>
            ) : (
                <div className="h-[50px] bg-bg-secondary flex">
                    <div className="flex-1"></div>
                    <div className="my-auto">
                        <Link to="/login" className="pr-[30px]">
                            <ButtonText>Login</ButtonText>
                        </Link>
                        <Link to="/register" className="pr-[0px]">
                            <ButtonText>Register</ButtonText>
                        </Link>
                    </div>
                    <div className="mt-4 mx-7 flex flex-col items-end w-[30px]">
                        <button>
                            <span
                                className="material-symbols-outlined cursor-pointer select-none"
                                onClick={() => setState(state == 2 ? 0 : 2)}>
                                settings
                            </span>
                        </button>
                        <button
                            onClick={() => {
                                toggleDarkMode(!(darkmode ?? false));
                                setState(0);
                            }}>
                            {state === 2 && (
                                <ul className="relative w-[200px] mt-4 bg-bg-secondary p-3">
                                    {darkmode ?? false ? (
                                        <span className="material-symbols-outlined align-middle pr-3">
                                            light_mode
                                        </span>
                                    ) : (
                                        <span className="material-symbols-outlined align-middle pr-3">
                                            dark_mode
                                        </span>
                                    )}
                                    Toggle {darkmode ?? false ? 'light' : 'dark'} mode
                                </ul>
                            )}
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
}

export default Header;
