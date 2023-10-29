/**
 * @jest-environment jsdom
 */
import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import ChatConversationSidebar from './ChatConversationSidebar';
import { BrowserRouter } from 'react-router-dom';

describe('ChatConversationSidebar', () => {
    test('it displays the chat sidebar', () => {
        render(
            <BrowserRouter>
                <ChatConversationSidebar 
                    search=""
                    setSearch={jest.fn()}
                    conversations={[]}
                    setConversations={jest.fn()}
                    currentConversation={undefined}
                    setCurrentConversation={jest.fn()}
                />
            </BrowserRouter>
        );

        // Expect the sidebar to be in the document
        expect(screen.getByPlaceholderText('Search...')).toBeInTheDocument();
    });
});
