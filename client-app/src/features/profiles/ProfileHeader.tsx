import React from "react";
import { Divider, Grid, Header, Item, Segment, Statistic, StatisticGroup } from "semantic-ui-react";
import { Profile } from "../../app/models/profile";
import { observer } from "mobx-react-lite";
import FollowButton from "./FollowButton";

interface Props {
    profile: Profile;
}

export default observer(function ProfileHeader({profile}: Props) {
    return (
        <Segment>
            <Grid>
                <Grid.Column width={12}>
                    <Item.Group>
                        <Item>
                            <Item.Image avatr size = 'small' src={profile.image || '/assets/user.png'} />
                            <Item.Content verticalAlign = 'middle'>
                                <Header as='h1' contnet={profile.displayName} />
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Grid.Column>
                <Grid.Column width = {4}>
                    <StatisticGroup widths={2}>
                        <Statistic label = 'Followers' value= {profile.followersCount} />
                        <Statistic label = 'Following' value={profile.followingCount} />
                    </StatisticGroup>
                    <Divider />
                    <FollowButton profile={profile} />
                </Grid.Column>
            </Grid>
        </Segment>
    )
})