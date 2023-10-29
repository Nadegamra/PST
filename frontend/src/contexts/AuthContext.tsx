import React, { useState, useContext, createContext, useEffect, useMemo } from 'react';
import { getProfile, login, logout } from '../api/AuthApi';
import { UserGet, UserLogin } from '../models/User';
import { useNavigate } from 'react-router';

export interface AuthContextProps {
    user?: UserGet;
    loading: boolean;
    login: (loginData: UserLogin) => Promise<string>;
    logout: () => void;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [user, setUser] = useState<UserGet | undefined>();
    const [loading, setLoading] = useState<boolean>(false);
    const [loadingInitial, setLoadingInitial] = useState<boolean>(true);
    const navigate = useNavigate();

    useEffect(() => {
        getProfile()
            .then((result) => {
                if (result.data === '') {
                    setUser(undefined);
                } else {
                    setUser(result.data);
                }
            })
            .finally(() => setLoadingInitial(false));
    }, [loading]);

    async function handleLogin(loginData: UserLogin): Promise<string> {
        setLoading(true);

        return login(loginData)
            .then((result) => {
                setUser(result.data);
                setLoading(false);
                navigate('/');
                return '';
            })
            .catch((error) => {
                setUser(undefined);
                setLoading(false);
                console.log(error);
                return error.response === undefined ? error.message : error.response.data;
            });
    }

    function handleLogout() {
        logout()
            .then(() => {
                setUser(undefined);
            })
            .finally(() => navigate('/'));
    }

    const memoedValue = useMemo(
        () => ({
            user,
            loading,
            login: handleLogin,
            logout: handleLogout,
        }),
        [user, loading]
    );

    return (
        <AuthContext.Provider value={memoedValue}>
            {!loadingInitial && children}
        </AuthContext.Provider>
    );
}

export { AuthContext };

