import { UserConsoleStatus } from '../models/UserConsole'
import { useEffect, useState } from 'react'
import { useAuth } from '../contexts/AuthContext'
import UserConsolesStatusSelectionAdmin from '../components/userConsoles/UserConsolesStatusSelectionAdmin'
import UserConsolesGrid from '../components/userConsoles/UserConsolesGrid'

function UserConsolesPage() {
    const [status, setStatus] = useState<UserConsoleStatus>(UserConsoleStatus.UNCONFIRMED)

    const { user } = useAuth()
    return (
        <div id="userConsolesContainer" className="flex-1">
            <div className="pt-3" id="adminUserConsolesButtons">
                {user?.role === 'admin' && (
                    <UserConsolesStatusSelectionAdmin status={status} setStatus={setStatus} />
                )}
            </div>
            <UserConsolesGrid status={status} />
        </div>
    )
}

export default UserConsolesPage
