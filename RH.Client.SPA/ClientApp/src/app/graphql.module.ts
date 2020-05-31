import { NgModule } from '@angular/core'
import { environment } from 'src/environments/environment'
import { ApolloModule, APOLLO_OPTIONS } from 'apollo-angular'
import { ApolloLink } from 'apollo-link'
import { InMemoryCache } from 'apollo-cache-inmemory'
import { HttpLinkModule, HttpLink } from 'apollo-angular-link-http'
import { AuthService } from './services/auth.service'

const uri = `${environment.apiUri}/graphql`

export function createApollo(httpLink: HttpLink, authService: AuthService) {
    const authLink = new ApolloLink((operation, forward) => {
        operation.setContext({
            headers: {
                'Authorization': authService.authorizationHeaderValue
            }
        })

        return forward(operation)
    })

    return {
        link: authLink.concat(httpLink.create({ uri: uri })),
        cache: new InMemoryCache({
            addTypename: false
        })
    }
}

@NgModule({
    exports: [ApolloModule, HttpLinkModule],
    providers: [
        {
            provide: APOLLO_OPTIONS,
            useFactory: createApollo,
            deps: [HttpLink, AuthService],
        },
    ],
})
export class GraphQLModule { }
