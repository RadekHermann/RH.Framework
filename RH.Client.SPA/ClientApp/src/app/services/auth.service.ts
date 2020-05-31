import { Injectable } from '@angular/core'
import { BehaviorSubject } from 'rxjs'
import { UserManager, User } from 'oidc-client'
import { environment } from 'src/environments/environment'

@Injectable({ providedIn: 'root' })
export class AuthService {
    private authStatusSource = new BehaviorSubject<boolean>(false)
    public authStatus$ = this.authStatusSource.asObservable()

    private manager = new UserManager({
        authority: environment.identityUri,
        client_id: 'angular_spa',
        redirect_uri: `${environment.uri}/auth/signin-callback`,
        post_logout_redirect_uri: `${environment.uri}/auth/signout-callback`,
        response_type: 'code',
        scope: 'openid profile email api.read',
        filterProtocolClaims: true,
        loadUserInfo: true
    })

    private user: User | null

    constructor() {
        this.manager.getUser().then(user => {
            this.user = user
            this.authStatusSource.next(this.isAuthenticated())
        })
    }

    login(newAccount?: boolean, userName?: string) {
        const extraQueryParams = newAccount && userName ? {
            newAccount: newAccount,
            userName: userName
        } : {}

        return this.manager.signinRedirect({
            extraQueryParams
        })
    }

    async signInComplete() {
        this.user = await this.manager.signinRedirectCallback()
        this.authStatusSource.next(this.isAuthenticated())
    }

    async signOutComplete() {
        await this.manager.signoutRedirectCallback()
        await this.manager.clearStaleState()

        this.authStatusSource.next(this.isAuthenticated())
    }

    isAuthenticated(): boolean {
        return this.user != null && !this.user.expired
    }

    get authorizationHeaderValue(): string {

        return this.user ? `${this.user.token_type} ${this.user.access_token}` : ''
    }

    get name(): string {
        return this.user != null ? this.user.profile.name : ''
    }

    async signout() {
        await this.manager.signoutRedirect()
    }
}
