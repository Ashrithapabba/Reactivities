import { observer } from "mobx-react-lite";
import React from "react";
import { Card, Grid, Header, Tab, TabPane } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import ProfileCard from "./ProfileCard";

export default observer(function ProfileFollowings() {
    const {profileStore} = useStore();
    const {profile, followings, loadingFollowings, activeTab} = profileStore;

    return (
        <TabPane loading={loadingFollowings}>
            <Grid.Column width = {16}>
                <Header floated = 'left' icon = 'user' content={activeTab === 3 ? `People following ${profile?.displayName}` : `Prople ${profile?.displayName} is Following`} />
            </Grid.Column>
            <Grid.Column width = {16}>
                <Card.Group itemsPerRow={4}>
                    {followings.map(profile => (
                        <ProfileCard key = {profile.username} profile={profile} />
                    ))}
                </Card.Group>
            </Grid.Column>
        </TabPane>
    )
})