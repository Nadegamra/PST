/**
 * @jest-environment jsdom
 */
import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import ChatConversationContainer from './ChatConversationContainer';
import { AuthContext } from '../../contexts/AuthContext'; 


jest.mock('react-router', () => ({
    ...jest.requireActual('react-router'),
    useNavigate: () => jest.fn()
}));

describe('ChatConversationContainer', () => {
    test('it renders the chat container', () => {

        const user = {
            id: 1,
            role: 'admin',
            firstName: 'test',
            lastName: 'test',
            companyCode: '',
            companyName: '',
            username: '',
            email: 'test@a.a',
            emailConfirmed: true,
            country: 'Lietuva',
            county: 'Kaunas',
            city: 'Kaunas',
            streetAddress: 'Studentu g. 50',
            postalCode: '10001',
            isCompany: false
        };
        
        const mockAuthContextValue = {
            user,
            loading: false,  // Mocked loading state
            login: jest.fn(),  // Mocked login 
            logout: jest.fn()  // Mocked logout 
        };
        
        render(
            <AuthContext.Provider value={mockAuthContextValue}>
                <ChatConversationContainer 
                    conversation={undefined}
                    message="test message"
                    setMessage={jest.fn()}
                    updateConversations={jest.fn()}
                />
            </AuthContext.Provider>
        );

        const chatContainerElement = screen.getByText('Šiuo metu pokalbių nėra');
        expect(chatContainerElement).toBeInTheDocument();
    });
});
