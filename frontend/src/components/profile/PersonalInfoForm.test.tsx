/**
 * @jest-environment jsdom
 */
import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import PersonalInfoForm from './PersonalInfoForm'; 
import { AuthContext, AuthContextProps } from '../../contexts/AuthContext';

// Mock API calls
jest.mock('../../api/AuthApi', () => ({
    getProfile: jest.fn(() => Promise.resolve({ data: '' })),
    login: jest.fn(() => Promise.resolve({ data: '' })),
    logout: jest.fn(() => Promise.resolve({ data: '' }))
}));

jest.mock('react-i18next', () => ({
    useTranslation: () => ({
      t: (str: string) => str,
      i18n: {
        changeLanguage: () => new Promise(() => { }),
      },
    }),
  }));

describe('PersonalInfoForm', () => {
    it('should update firstName and lastName correctly', () => {
        const mockContextValue: AuthContextProps = {
            user: {
                "id": 1,
                "role": "admin",
                "firstName": "Admy",
                "lastName": "Nisterson",
                "companyCode": "",
                "companyName": "",
                "username": "admin@admin.com",
                "email": "admin@admin.com",
                "emailConfirmed": true,
                "country": "",
                "county": "",
                "city": "",
                "streetAddress": "",
                "postalCode": "",
                "isCompany": false
            },
            loading: false,
            login: jest.fn(),
            logout: jest.fn()
        };

        render(
            <AuthContext.Provider value={mockContextValue}>
                <PersonalInfoForm />
            </AuthContext.Provider>
        );
        
        const firstNameInput = screen.getByDisplayValue(mockContextValue.user!.firstName) as HTMLInputElement;
        const lastNameInput = screen.getByDisplayValue(mockContextValue.user!.lastName) as HTMLInputElement;
        
        fireEvent.change(firstNameInput, { target: { value: 'Jane' } });
        fireEvent.change(lastNameInput, { target: { value: 'Smith' } });

        expect(firstNameInput.value).toBe('Jane');
        expect(lastNameInput.value).toBe('Smith');
    });
});